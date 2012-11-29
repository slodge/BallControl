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
using Windows.Networking.Proximity;

namespace Cirrious.MvvmCross.Plugins.Sphero.WindowsPhone.Tooth
{
    public class SpheroFinder : ISpheroFinder
    {
        public void Find(Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError)
        {
            ThreadPool.QueueUserWorkItem(ignored => DoFind(onSuccess, onError));
        }

        private async void DoFind(Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError)
        {
            try
            {
                PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
                var peers = await PeerFinder.FindAllPeersAsync();
                var spheroPeers = peers
                    .Where(p => p.DisplayName.Contains("Sphero"))
                    .Select(p => new AvailableSphero(p))
                    .ToList<IAvailableSphero>();
                onSuccess(spheroPeers);
            }
            catch (Exception exception)
            {
                onError(exception);
            }
        }
    }
}