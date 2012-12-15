// <copyright file="StreamSocketWrapper.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Sphero.HackFileShare;

namespace Cirrious.MvvmCross.Plugins.Sphero.Wpf.Tooth
{
    public class StreamSocketWrapper : IStreamSocketWrapper, IDisposable
    {
        private readonly NetworkStream _spheroSocket;

        public StreamSocketWrapper(NetworkStream spheroSocket)
        {
            _spheroSocket = spheroSocket;
        }

        public async Task<byte> ReceiveByte()
        {
            try
            {
                var toReturn = new byte[1];
                while (true)
                {
                    var read = await _spheroSocket.ReadAsync(toReturn, 0, 1);
                    if (read <= 0)
                    {
                        continue;
                    }
                    return toReturn[0];
                }
            }
            catch (Exception exception)
            {
                throw new SpheroPluginException(exception, "Read failed - suspect disconnected");
            }
        }

        public async Task SendBytes(byte[] payload)
        {
            // TODO - should we somehow check length sent is payload.Length here
            await _spheroSocket.WriteAsync(payload, 0, payload.Length);
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