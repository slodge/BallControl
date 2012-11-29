// <copyright file="AvailableSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Android.Bluetooth;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Java.Util;

namespace Cirrious.MvvmCross.Plugins.Sphero.Droid.Tooth
{
    public class AvailableSphero : BaseSphero, IAvailableSphero
    {
        // from http://stackoverflow.com/questions/4632524/how-to-find-the-uuid-of-serial-port-bluetooth-device
        private static readonly UUID SppUuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");

        public AvailableSphero(BluetoothDevice bluetoothDevice)
            : base(bluetoothDevice)
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
                var socket = BluetoothDevice.CreateRfcommSocketToServiceRecord(SppUuid);
                socket.Connect();
                var connected = new ConnectedSphero(BluetoothDevice, socket);
                onSuccess(connected);
            }
            catch (Exception exception)
            {
                onError(exception);
            }
        }
    }
}