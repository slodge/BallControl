// <copyright file="SpheroHeadingViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Plugins.Sphero.Commands;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class SpheroHeadingViewModel : BaseSpheroChildViewModel
    {
        private bool _bacKLedOn;
        private int _currentHeading;

        public SpheroHeadingViewModel(ISpheroParentViewModel parent)
            : base(parent)
        {
        }

        public bool BackLedOn
        {
            get { return _bacKLedOn; }
            set
            {
                _bacKLedOn = value;
                RaisePropertyChanged(() => BackLedOn);
            }
        }

        public ICommand ToggleBackLedCommand
        {
            get { return new MvxRelayCommand(DoToggleBackLed); }
        }

        private void DoToggleBackLed()
        {
            BackLedOn = !BackLedOn;
            var command = new BackLedCommand(BackLedOn ? 255 : 0);
            SendCommand(command);
        }

        public ICommand SetHeadingCommand
        {
            get { return new MvxRelayCommand<int>(DoSetHeading); }
        }

        public ICommand ZeroRelativeHeadingCommand
        {
            get { return new MvxRelayCommand(DoZeroRelativeHeading); }
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