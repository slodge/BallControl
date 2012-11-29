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
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;

namespace Cirrious.MvvmCross.Plugins.Sphero.WindowsPhone.Tooth
{
    public class AvailableSphero : BaseSphero, IAvailableSphero
    {
        public AvailableSphero(PeerInformation peerInformation)
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
                var spheroSocket = new StreamSocket();

                await spheroSocket.ConnectAsync(PeerInformation.HostName, "1");
                onSuccess(new ConnectedSphero(PeerInformation, spheroSocket));

                /*
                var task = spheroSocket.ConnectAsync(PeerInformation.HostName, "1").AsTask();
                
                task.ContinueWith(r =>
                    {
                        onSuccess(new ConnectedSphero(PeerInformation, spheroSocket));
                    });
                */
            }
            catch (Exception exception)
            {
                onError(exception);
            }
        }
    }
}