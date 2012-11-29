// <copyright file="CommandWithActions.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;

namespace Cirrious.MvvmCross.Plugins.Sphero.Helpers
{
    public class CommandWithActions
    {
        public CommandWithActions(ISpheroCommand command, Action<ISpheroMessage> onSuccess, Action<Exception> onError)
        {
            OnError = onError;
            OnSuccess = onSuccess;
            Command = command;
        }

        public ISpheroCommand Command { get; private set; }
        public Action<ISpheroMessage> OnSuccess { get; private set; }
        public Action<Exception> OnError { get; private set; }
    }
}