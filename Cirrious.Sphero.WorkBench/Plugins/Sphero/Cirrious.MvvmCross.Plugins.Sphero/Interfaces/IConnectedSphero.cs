// <copyright file="IConnectedSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;

namespace Cirrious.MvvmCross.Plugins.Sphero.Interfaces
{
    public interface IConnectedSphero : IBaseSphero
    {
        bool IsConnected { get; }
        void SendAndReceive(ISpheroCommand command, Action<ISpheroMessage> onSuccess, Action<Exception> onError);
        void Disconnect();
        event EventHandler Disconnected;
        // TODO... more will be needed here?
    }
}