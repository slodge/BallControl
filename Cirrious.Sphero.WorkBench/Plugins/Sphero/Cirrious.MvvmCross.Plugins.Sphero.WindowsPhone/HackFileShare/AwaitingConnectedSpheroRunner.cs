// <copyright file="AwaitingConnectedSpheroRunner.cs" company="Cirrious">
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
using System.Threading.Tasks;
#if NETFX_CORE
using Windows.System.Threading;
#endif
using Cirrious.MvvmCross.Plugins.Sphero.Helpers;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.Plugins.Sphero.Messages;

// ReSharper disable CheckNamespace

namespace Cirrious.MvvmCross.Plugins.Sphero.HackFileShare
// ReSharper restore CheckNamespace
{
    public class AwaitingConnectedSpheroRunner
    {
        private const int MaxQueueSize = 250;

        private byte _sequenceNumber;
        private bool _disconnected;
        private readonly Queue<CommandWithActions> _commandsToSend;
        private readonly SemaphoreSlim _itemsToSendEvent;
        private readonly IStreamSocketWrapper _streamSpheroWrapper;

        private readonly Dictionary<int, CommandWithActions> _responseListeners =
            new Dictionary<int, CommandWithActions>();

        public AwaitingConnectedSpheroRunner(IStreamSocketWrapper streamSpheroWrapper)
        {
            _streamSpheroWrapper = streamSpheroWrapper;
            _itemsToSendEvent = new SemaphoreSlim(1);
            _itemsToSendEvent.Wait();
            _commandsToSend = new Queue<CommandWithActions>();
        }

        public void Start()
        {
#if NETFX_CORE
            ThreadPool.RunAsync(ignored => ReceiveResponses());
            ThreadPool.RunAsync(ignored => SendCommmands());
#else
            ThreadPool.QueueUserWorkItem(ignored => ReceiveResponses());
            ThreadPool.QueueUserWorkItem(ignored => SendCommmands());
#endif
        }

        public void SendAndReceive(ISpheroCommand command, Action<ISpheroMessage> onSuccess, Action<Exception> onError)
        {
            //DoCommand(command, onSuccess, onError);
            var message = new CommandWithActions(command, onSuccess, onError);

            lock (_commandsToSend)
            {
                _commandsToSend.Enqueue(message);
                if (_commandsToSend.Count == 1)
                {
                    _itemsToSendEvent.Release();
                }
                while (_commandsToSend.Count > MaxQueueSize)
                {
                    // TODO - shoudl at least trace this really!
                    _commandsToSend.Dequeue();
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

        private async Task SendCommand(CommandWithActions toSend)
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
                await _streamSpheroWrapper.SendBytes(payload);
            }
            catch (Exception exception)
            {
                toSend.OnError(exception);
            }
        }

        private async Task<SpheroResponse> ReceiveResponse()
        {
            var builder = new SpheroResponseBuilder();

            while (true)
            {
                var x = await _streamSpheroWrapper.ReceiveByte();
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

        protected async Task ReceiveResponses()
        {
            try
            {
                while (true)
                {
                    var response = await ReceiveResponse();
                    if (response.IsStreamingResponse)
                    {
                        // TODO
                    }
                    else
                    {
                        await ProcessCommandResponse(response);
                    }
                }
            }
            catch (SpheroPluginException exception)
            {
                // assume disconnected
                RaiseDisconnected();
            }
        }

        protected async Task SendCommmands()
        {
            try
            {
                while (true)
                {
                    var waitResult = await _itemsToSendEvent.WaitAsync(1000);
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
                        if (_commandsToSend.Count != 0)
                        {
                            _itemsToSendEvent.Release();
                        }
                    }

                    await SendCommand(toSendThisTime);
                }
            }
            catch (SpheroPluginException exception)
            {
                // assume disconnected
                RaiseDisconnected();
            }
        }

#warning dead code - kill it
        /*
        private async Task SendMessage()
        {
            try
            {
                while (true)
                {
                    var response = await ReceiveResponse();
                    if (response.IsStreamingResponse)
                    {
                        // TODO
                    }
                    else
                    {
                        await ProcessCommandResponse(response);
                    }
                }
            }
            catch (SpheroPluginException exception)
            {
                // assume disconnected
                RaiseDisconnected();
            }
        }
        */

        private async Task ProcessCommandResponse(SpheroResponse response)
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