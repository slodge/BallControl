using System;
using System.Collections.Generic;
using System.Threading;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.Sphero.WorkBench.Core.Interfaces;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels
{
    public class GangnamStyleViewModel : BaseViewModel
    {
        private int _failedCount;
        private readonly List<IAvailableSphero> _availableSpheros;
        private readonly List<IConnectedSphero> _connectedSpheros;

        public GangnamStyleViewModel()
        {
            _connectedSpheros = new List<IConnectedSphero>();
            _availableSpheros = new List<IAvailableSphero>();

            var listService = Mvx.Resolve<ISpheroListService>();
            var availableSpheros = listService.AvailableSpheros;
            if (availableSpheros != null)
                _availableSpheros.AddRange(availableSpheros);

            ThreadPool.QueueUserWorkItem(ignored => DoConnect());
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; RaisePropertyChanged(() => IsConnected); }
        }

        private void ClearConnectedSpheros()
        {
            lock (this)
            {
                foreach (var connectedSphero in _connectedSpheros)
                {
                    connectedSphero.Disconnected -= ConnectedSpheroOnDisconnected;
                    connectedSphero.Disconnect();
                }
                _connectedSpheros.Clear();
            }
        }

        public void Shutdown()
        {
            ClearConnectedSpheros();
        }

        private void DoConnect()
        {
            foreach (var availableSphero in _availableSpheros)
            {
                availableSphero.Connect(OnConnectionSuccess, OnConnectionError);
            }
        }

        private void OnConnectionSuccess(IConnectedSphero sphero)
        {
            lock (this)
            {
                _connectedSpheros.Add(sphero);
            }

            // if we've got at least one connection...
            IsConnected = true;
        }

        private void ConnectedSpheroOnDisconnected(object sender, EventArgs eventArgs)
        {
            // ignore...
        }

        private void OnConnectionError(Exception obj)
        {
            // TODO - signal the error  
            lock (this)
            {
                _failedCount++;
            }
        }

        public void SendCommand(ISpheroCommand command)
        {
            lock (this)
            {
                foreach (var connectedSphero in _connectedSpheros)
                {
                    SendCommand(connectedSphero, command);
                }
            }
        }

        private void SendCommand(IConnectedSphero sphero, ISpheroCommand command)
        {
            if (sphero == null)
            {
                // TODO - trace this ignore
                SpheroTrace.Trace("Ignoring command {0}", command);
                return;
            }

            //SpheroTrace.Trace("Sending command {0}", command);
            sphero.SendAndReceive(command, OnCommandSuccess, OnCommandError);
        }

        protected virtual void OnCommandError(Exception obj)
        {
            // TODO - report this error
        }

        protected virtual void OnCommandSuccess(ISpheroMessage message)
        {
            // TODO - do we care about this?
        }
    }
}