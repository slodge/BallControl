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
using MonoTouch.ExternalAccessory;
using MonoTouch.Foundation;

namespace Cirrious.MvvmCross.Plugins.Sphero.Touch.Tooth
{
    public class StreamSocketWrapper : IStreamSocketWrapper, IDisposable
    {
        private readonly EASession _spheroSocket;

        public StreamSocketWrapper(EASession spheroSocket)
        {
            _spheroSocket = spheroSocket;

			_spheroSocket.InputStream.OnEvent += DataReceived;
			_spheroSocket.InputStream.Schedule(NSRunLoop.Current, "kCFRunLoopDefaultMode");                                                      
			_spheroSocket.InputStream.Open();     
			
			_spheroSocket.OutputStream.Schedule(NSRunLoop.Current, "kCFRunLoopDefaultMode"); 
			_spheroSocket.OutputStream.Open();
        }

		void DataReceived (object sender, NSStreamEventArgs e)
		{
			// TODO - ignored for now...
			// throw new NotImplementedException ();
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
                _spheroSocket.InputStream.Read(buffer, 1);
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
			// TODO - should probably check what Write returns and maybe do a loop to send everything
            _spheroSocket.OutputStream.Write(payload, (uint)payload.Length);
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