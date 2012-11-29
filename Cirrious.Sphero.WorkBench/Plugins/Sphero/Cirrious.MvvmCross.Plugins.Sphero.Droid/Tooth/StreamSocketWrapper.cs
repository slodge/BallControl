// <copyright file="StreamSocketWrapper.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Threading.Tasks;
using Android.Bluetooth;
using Cirrious.MvvmCross.Plugins.Sphero.HackFileShare;

namespace Cirrious.MvvmCross.Plugins.Sphero.Droid.Tooth
{
    public class StreamSocketWrapper : IStreamSocketWrapper, IDisposable
    {
        private readonly BluetoothSocket _spheroSocket;

        public StreamSocketWrapper(BluetoothSocket spheroSocket)
        {
            _spheroSocket = spheroSocket;
        }

        public Task<byte> ReceiveByte()
        {
            return new Task<byte>(DoReceiveByte);
        }

        private byte DoReceiveByte()
        {
            var buffer = new byte[1];
            try
            {
                _spheroSocket.InputStream.Read(buffer, 0, 1);
            }
            catch (Exception exception)
            {
                throw new SpheroPluginException(exception, "Read failed - suspect disconnected");
            }

            return buffer[0];
        }

        public Task SendBytes(byte[] payload)
        {
            return new Task(() => DoSendBytes(payload));
        }

        private void DoSendBytes(byte[] payload)
        {
            _spheroSocket.OutputStream.Write(payload, 0, payload.Length);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _spheroSocket.Dispose();
            }
        }
    }
}