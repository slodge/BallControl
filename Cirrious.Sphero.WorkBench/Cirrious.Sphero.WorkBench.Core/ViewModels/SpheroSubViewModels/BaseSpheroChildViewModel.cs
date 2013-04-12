// <copyright file="BaseSpheroChildViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.Sphero.WorkBench.Core.Interfaces;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class BaseSpheroChildViewModel
        : BaseViewModel
          , ISpheroChildViewModel
    {
        public ISpheroParentViewModel Parent { get; private set; }

        private readonly ISpheroSpeedService _speedService;

        public ISpheroSpeedService SpeedService
        {
            get { return _speedService; }
        }

        protected BaseSpheroChildViewModel(ISpheroParentViewModel parent)
        {
            Parent = parent;
            _speedService = Mvx.Resolve<ISpheroSpeedService>();
        }

        public virtual void OnSpheroConnected()
        {
            // does nothing by default
        }

        protected void SendCommand(ISpheroCommand command)
        {
            var sphero = Parent.ConnectedSphero;
            if (sphero == null)
            {
                // TODO - trace this ignore
                SpheroTrace.Trace("Ignoring command {0}", command);
                return;
            }

			SpheroTrace.Trace("Sending command {0}", command);
            sphero.SendAndReceive(command, OnCommandSuccess, OnCommandError);
        }

        protected virtual void OnCommandError(Exception obj)
        {
            // TODO - report this error
        }

        protected virtual void OnCommandSuccess(ISpheroMessage message)
        {
            // TODO - do we care about this?
        }
    }
}