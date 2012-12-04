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
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Windows.Networking.Proximity;
using Windows.System.Threading;

namespace Cirrious.MvvmCross.Plugins.Sphero.WinRT.Tooth
{
    public class SpheroFinder : ISpheroFinder
    {
        public void Find(Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError)
        {
            ThreadPool.RunAsync(ignored => DoFind(onSuccess, onError));
        }

        private async void DoFind(Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError)
        {
            try
            {
                // what?!
                var list = HackSingleton.Instance.Service.GetAvailableSpheroNames();

                var i = 12;
                /*
                PeerFinder.Start("Bluetooth:Paired");
                PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
                var peers = await PeerFinder.FindAllPeersAsync();
                var spheroPeers = peers
                    .Where(p => p.DisplayName.Contains("Sphero"))
                    .Select(p => new AvailableSphero(p))
                    .ToList<IAvailableSphero>();
                onSuccess(spheroPeers);
                 */
            }
            catch (Exception exception)
            {
                onError(exception);
            }
        }
    }
}