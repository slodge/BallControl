// <copyright file="SpheroFinder.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using MonoTouch.ExternalAccessory;

namespace Cirrious.MvvmCross.Plugins.Sphero.Touch.Tooth
{
    public class SpheroFinder : ISpheroFinder
    {
		// TODO - could later add dynamic detection using code like:
		// accessoryConnectedObserver = NSNotificationCenter.DefaultCenter.AddObserver(new NSString("EAAccessoryDidConnectNotification"), AccessoryConnected);
		// EAAccessoryManager.SharedAccessoryManager.RegisterForLocalNotifications();

        public void Find(Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError)
        {
            ThreadPool.QueueUserWorkItem(ignored =>
                {
                    try
                    {
						var manager = EAAccessoryManager.SharedAccessoryManager;

						var devices = manager.ConnectedAccessories
						.Where(p => p.ProtocolStrings.Contains(Constants.ProtocolName))
                                     .Select(p => new AvailableSphero(p) as IAvailableSphero)
                                     .ToList();
                        onSuccess(devices);
                    }
                    catch (Exception exception)
                    {
                        onError(exception);
                    }
                });
        }
    }
}