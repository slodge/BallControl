// <copyright file="BaseSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Windows.Networking.Proximity;

namespace Cirrious.MvvmCross.Plugins.Sphero.WinRT.Tooth
{
    public class BaseSphero : IBaseSphero
    {
        private readonly PeerInformation _peerInformation;

        public BaseSphero(PeerInformation peerInformation)
        {
            _peerInformation = peerInformation;
        }

        public string Name
        {
            get { return _peerInformation.DisplayName; }
        }

        protected PeerInformation PeerInformation
        {
            get { return _peerInformation; }
        }
    }
}