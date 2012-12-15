// <copyright file="AvailableSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Threading;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace Cirrious.MvvmCross.Plugins.Sphero.Wpf.Tooth
{
    public class AvailableSphero : BaseSphero, IAvailableSphero
    {
        public AvailableSphero(BluetoothDeviceInfo peerInformation)
            : base(peerInformation)
        {
        }

        public void Connect(Action<IConnectedSphero> onSuccess, Action<Exception> onError)
        {
            ThreadPool.QueueUserWorkItem(ignored => DoConnect(onSuccess, onError));
        }

        private async void DoConnect(Action<IConnectedSphero> onSuccess, Action<Exception> onError)
        {
            try
            {
                var serviceClass = BluetoothService.SerialPort;
                var ep = new BluetoothEndPoint(PeerInformation.DeviceAddress, serviceClass);
                var client = new BluetoothClient();
                client.Connect(ep);
                var stream = client.GetStream();
                onSuccess(new ConnectedSphero(PeerInformation, stream));
            }
            catch (Exception exception)
            {
                onError(exception);
            }
        }
    }
}