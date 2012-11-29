// <copyright file="NonAwaitingConnectedSpheroRunner.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;
using System.Threading;
using Cirrious.MvvmCross.Plugins.Sphero.Helpers;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.Plugins.Sphero.Messages;

// ReSharper disable CheckNamespace

namespace Cirrious.MvvmCross.Plugins.Sphero.HackFileShare
// ReSharper restore CheckNamespace
{
    public class NonAwaitingConnectedSpheroRunner
    {
        private byte _sequenceNumber;
        private bool _disconnected;
        private readonly Queue<CommandWithActions> _commandsToSend;
        private readonly ManualResetEvent _itemsReadyEvent;
        private readonly IStreamSocketWrapper _streamSpheroWrapper;

        private readonly Dictionary<int, CommandWithActions> _responseListeners =
            new Dictionary<int, CommandWithActions>();

        public NonAwaitingConnectedSpheroRunner(IStreamSocketWrapper streamSpheroWrapper)
        {
            _streamSpheroWrapper = streamSpheroWrapper;
            _itemsReadyEvent = new ManualResetEvent(false);
            _commandsToSend = new Queue<CommandWithActions>();
        }

        public void Start()
        {
            var receiveThread = new Thread(ReceiveResponses);
            var sendThread = new Thread(SendCommmands);
            receiveThread.Start();
            sendThread.Start();
        }

        public void SendAndReceive(ISpheroCommand command, Action<ISpheroMessage> onSuccess, Action<Exception> onError)
        {
            //DoCommand(command, onSuccess, onError);
            var message = new CommandWithActions(command, onSuccess, onError);

            lock (_commandsToSend)
            {
                _commandsToSend.Enqueue(message);
                if (_commandsToSend.Count > 0)
                {
                    _itemsReadyEvent.Set();
                }
            }
        }

        public void Disconnect()
        {
            lock (_responseListeners)
            {
                _responseListeners.Clear();
            }
            _disconnected = true;
        }

        private void SendCommand(CommandWithActions toSend)
        {
            try
            {
                int sequenceNumber;
                lock (this)
                {
                    sequenceNumber = _sequenceNumber;
                    _sequenceNumber++;

                    _responseListeners[sequenceNumber] = toSend;
                }

                byte[] payload = toSend.Command.GetBytes(sequenceNumber);
                var sendTask = _streamSpheroWrapper.SendBytes(payload);
                sendTask.Start();
                sendTask.Wait();
            }
            catch (AggregateException exception)
            {
                toSend.OnError(exception.InnerException);
            }
            catch (Exception exception)
            {
                toSend.OnError(exception);
            }
        }

        private SpheroResponse ReceiveResponse()
        {
            var builder = new SpheroResponseBuilder();

            while (true)
            {
                byte x;

                try
                {
                    var task = _streamSpheroWrapper.ReceiveByte();
                    task.Start();
                    x = task.Result;
                }
                catch (AggregateException exception)
                {
                    throw exception.InnerException;
                }

                builder.Add(x);

                if (builder.IsErrored)
                {
                    throw new SpheroPluginException("Error reading packet {0}", builder.CurrentState);
                }

                if (builder.IsComplete)
                {
                    return builder.Response;
                }
            }
        }

        private void ReceiveResponses()
        {
            try
            {
                while (true)
                {
                    if (_disconnected)
                    {
                        break;
                    }

                    var response = ReceiveResponse();
                    if (response.IsStreamingResponse)
                    {
                        // TODO
                    }
                    else
                    {
                        ProcessCommandResponse(response);
                    }
                }
            }
            catch (SpheroPluginException exception)
            {
                // assume disconnected
                RaiseDisconnected();
            }
        }

        private void SendCommmands()
        {
            try
            {
                while (true)
                {
                    var waitResult = _itemsReadyEvent.WaitOne(100);

                    if (!waitResult)
                    {
                        if (_disconnected)
                        {
                            break;
                        }
                        continue;
                    }

                    CommandWithActions toSendThisTime = null;
                    lock (_commandsToSend)
                    {
                        toSendThisTime = _commandsToSend.Dequeue();
                        if (_commandsToSend.Count == 0)
                        {
                            _itemsReadyEvent.Reset();
                        }
                    }

                    SendCommand(toSendThisTime);
                }
            }
            catch (SpheroPluginException exception)
            {
                // assume disconnected
                RaiseDisconnected();
            }
        }

        private void ProcessCommandResponse(SpheroResponse response)
        {
            CommandWithActions actions;
            lock (this)
            {
                if (!_responseListeners.TryGetValue(response.SequenceNumber, out actions))
                {
                    // TODO - some trace here would be fab
                    return;
                }

                _responseListeners.Remove(response.SequenceNumber);
            }

            if (actions == null)
                return;

            try
            {
                ISpheroMessage message = actions.Command.ProcessResponse(response);
                actions.OnSuccess(message);
            }
            catch (Exception exception)
            {
                actions.OnError(exception);
            }
        }

        public event EventHandler Disconnected;

        public void RaiseDisconnected()
        {
            var handler = Disconnected;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}