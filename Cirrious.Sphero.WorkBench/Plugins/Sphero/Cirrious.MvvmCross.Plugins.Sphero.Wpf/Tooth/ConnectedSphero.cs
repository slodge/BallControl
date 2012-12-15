// <copyright file="ConnectedSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Net.Sockets;
using Cirrious.MvvmCross.Plugins.Sphero.HackFileShare;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using InTheHand.Net.Sockets;

namespace Cirrious.MvvmCross.Plugins.Sphero.Wpf.Tooth
{
    public class ConnectedSphero
        : BaseSphero, IConnectedSphero
    {
        private readonly StreamSocketWrapper _spheroSocketWrapper;
        private readonly AwaitingConnectedSpheroRunner _runner;

        public ConnectedSphero(BluetoothDeviceInfo peerInformation, NetworkStream spheroSocket)
            : base(peerInformation)
        {
            _spheroSocketWrapper = new StreamSocketWrapper(spheroSocket);
            _runner = new AwaitingConnectedSpheroRunner(_spheroSocketWrapper);
            _runner.Disconnected += (sender, args) => RaiseDisconnected();
            _runner.Start();
        }

#warning TODO - Clearly IsConnected could do better....
        public bool IsConnected
        {
            get { return true; }
        }

        public void SendAndReceive(ISpheroCommand command, Action<ISpheroMessage> onSuccess, Action<Exception> onError)
        {
            _runner.SendAndReceive(command, onSuccess, onError);
        }

        public void Disconnect()
        {
            _runner.Disconnect();
            _spheroSocketWrapper.Dispose();
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