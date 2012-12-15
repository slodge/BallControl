// <copyright file="BaseSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using InTheHand.Net.Sockets;

namespace Cirrious.MvvmCross.Plugins.Sphero.Wpf.Tooth
{
    public class BaseSphero : IBaseSphero
    {
        private readonly BluetoothDeviceInfo _peerInformation;

        public BaseSphero(BluetoothDeviceInfo peerInformation)
        {
            _peerInformation = peerInformation;
        }

        public string Name
        {
            get { return _peerInformation.DeviceName; }
        }

        protected BluetoothDeviceInfo PeerInformation
        {
            get { return _peerInformation; }
        }
    }
}