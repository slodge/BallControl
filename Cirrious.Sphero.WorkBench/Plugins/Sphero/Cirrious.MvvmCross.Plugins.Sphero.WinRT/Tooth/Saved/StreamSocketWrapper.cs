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
using Cirrious.MvvmCross.Plugins.Sphero.HackFileShare;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace Cirrious.MvvmCross.Plugins.Sphero.WinRT.Tooth
{
    public class StreamSocketWrapper : IStreamSocketWrapper, IDisposable
    {
        private readonly StreamSocket _spheroSocket;

        public StreamSocketWrapper(StreamSocket spheroSocket)
        {
            _spheroSocket = spheroSocket;
        }

        public async Task<byte> ReceiveByte()
        {
            IBuffer buffer;
            try
            {
                buffer =
                    await
                    _spheroSocket.InputStream.ReadAsync(new Windows.Storage.Streams.Buffer(1), 1,
                                                        InputStreamOptions.None);
            }
            catch (Exception exception)
            {
                throw new SpheroPluginException(exception, "Read failed - suspect disconnected");
            }

            var x = buffer.GetByteFromBuffer();
            return x;
        }

        public async Task SendBytes(byte[] payload)
        {
            var buffer = payload.GetBufferFromByteArray();
            await _spheroSocket.OutputStream.WriteAsync(buffer);
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