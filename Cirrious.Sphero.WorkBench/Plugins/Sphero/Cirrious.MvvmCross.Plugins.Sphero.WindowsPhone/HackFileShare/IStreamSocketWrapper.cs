// <copyright file="IStreamSocketWrapper.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Threading.Tasks;

// ReSharper disable CheckNamespace

namespace Cirrious.MvvmCross.Plugins.Sphero.HackFileShare
// ReSharper restore CheckNamespace
{
    public interface IStreamSocketWrapper
    {
        Task<byte> ReceiveByte();
        Task SendBytes(byte[] payload);
    }
}