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
using Android.Bluetooth;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;

namespace Cirrious.MvvmCross.Plugins.Sphero.Droid.Tooth
{
    public class SpheroFinder : ISpheroFinder
    {
        public void Find(Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError)
        {
            ThreadPool.QueueUserWorkItem(ignored =>
                {
                    try
                    {
                        // Get the local Bluetooth adapter
                        var adapter = BluetoothAdapter.DefaultAdapter;
                        var devices = adapter.BondedDevices
                                             .Where(p => p.Name.Contains("Sphero"))
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