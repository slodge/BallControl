// <copyright file="AvailableSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Cirrious.MvvmCross.Exceptions;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.System.Threading;

namespace Cirrious.MvvmCross.Plugins.Sphero.WinRT.Tooth
{
    public class AvailableSphero : BaseSphero, IAvailableSphero
    {
        public AvailableSphero(string name)
            : base(name)
        {
        }

        public void Connect(Action<IConnectedSphero> onSuccess, Action<Exception> onError)
        {
            ThreadPool.RunAsync(ignored => DoConnect(onSuccess, onError));
        }

        private async void DoConnect(Action<IConnectedSphero> onSuccess, Action<Exception> onError)
        {
            try
            {
                var success = HackSingleton.Instance.Service.ConnectToSphero(Name);

                if (success)
                {
                    onSuccess(new ConnectedSphero(Name));
                }
                else
                {
                    onError(new SpheroPluginException("Failed to connect"));
                }
            }
            catch (Exception exception)
            {
                onError(exception);
            }
        }
    }
}