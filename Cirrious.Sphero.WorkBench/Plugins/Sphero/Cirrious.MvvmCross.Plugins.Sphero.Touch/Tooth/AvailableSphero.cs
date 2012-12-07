// <copyright file="AvailableSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using MonoTouch.ExternalAccessory;
using MonoTouch.Foundation;

namespace Cirrious.MvvmCross.Plugins.Sphero.Touch.Tooth
{
    public class AvailableSphero : BaseSphero, IAvailableSphero
    {
        public AvailableSphero(EAAccessory accessory)
            : base(accessory)
        {
        }

        public void Connect(Action<IConnectedSphero> onSuccess, Action<Exception> onError)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(ignored => DoConnect(onSuccess, onError));
        }

        private void DoConnect(Action<IConnectedSphero> onSuccess, Action<Exception> onError)
        {
            try
            {
				var session = new EASession(Accessory, Constants.ProtocolName);
				var connected = new ConnectedSphero(Accessory, session);
                onSuccess(connected);
            }
            catch (Exception exception)
            {
                onError(exception);
            }
        }
    }
}