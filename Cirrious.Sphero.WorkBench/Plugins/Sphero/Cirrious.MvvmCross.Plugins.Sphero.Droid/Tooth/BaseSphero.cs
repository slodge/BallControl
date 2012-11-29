// <copyright file="BaseSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Android.Bluetooth;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;

namespace Cirrious.MvvmCross.Plugins.Sphero.Droid.Tooth
{
    public class BaseSphero : IBaseSphero
    {
        private readonly BluetoothDevice _bluetoothDevice;

        protected BluetoothDevice BluetoothDevice
        {
            get { return _bluetoothDevice; }
        }

        protected BaseSphero(BluetoothDevice bluetoothDevice)
        {
            _bluetoothDevice = bluetoothDevice;
        }

        public string Name
        {
            get { return _bluetoothDevice.Name; }
        }
    }
}