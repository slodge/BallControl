// <copyright file="SpheroHeadingViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Sphero.Commands;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class SpheroHeadingViewModel : BaseSpheroChildViewModel
    {
        private int _currentHeading;

        public SpheroHeadingViewModel(ISpheroParentViewModel parent)
            : base(parent)
        {
        }

        public ICommand SetHeadingCommand
        {
            get { return new MvxCommand<int>(DoSetHeading); }
        }

        public ICommand ZeroRelativeHeadingCommand
        {
            get { return new MvxCommand(DoZeroRelativeHeading); }
        }

        private void DoZeroRelativeHeading()
        {
            _currentHeading = 0;
        }

        private void DoSetHeading(int newHeadingDegrees)
        {
            var headingChange = newHeadingDegrees - _currentHeading;

            while (headingChange >= 360)
                headingChange -= 360;
            while (headingChange < 0)
                headingChange += 360;

            if (headingChange < 15.0 || headingChange > 345.0)
                return;

            _currentHeading = newHeadingDegrees;

            SpheroTrace.Trace("Heading to {0})", headingChange);
            var command = new SetHeadingCommand(headingChange);
            SendCommand(command);
        }
    }
}