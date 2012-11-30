// <copyright file="BufferExtensionMethods.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Windows.Storage.Streams;

// ReSharper disable CheckNamespace
namespace Cirrious.MvvmCross.Plugins.Sphero.HackFileShare
// ReSharper restore CheckNamespace
{
    public static class BufferExtensionMethods
    {
        public static byte GetByteFromBuffer(this IBuffer buffer)
        {
            using (var dr = DataReader.FromBuffer(buffer))
            {
                return dr.ReadByte();
            }
        }

        public static IBuffer GetBufferFromByteArray(this byte[] payload)
        {
            using (var dw = new DataWriter())
            {
                dw.WriteBytes(payload);
                return dw.DetachBuffer();
            }
        }
    }
}