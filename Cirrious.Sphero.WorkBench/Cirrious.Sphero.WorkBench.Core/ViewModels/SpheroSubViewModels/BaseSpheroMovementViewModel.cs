// <copyright file="BaseSpheroMovementViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Sphero.Commands;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class BaseSpheroMovementViewModel : BaseSpheroChildViewModel
    {
        private const double DistanceThreshold = 0.05;

        private RelativePositionParameters _position;

        protected BaseSpheroMovementViewModel(ISpheroParentViewModel parent) : base(parent)
        {
            Position = new RelativePositionParameters();
        }

        public RelativePositionParameters Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged(() => Position);
            }
        }

        protected void DoRoll(RelativePositionParameters relativePositionParameters)
        {
            if (Position.SimpleDistanceFrom(relativePositionParameters) < DistanceThreshold)
            {
                return;
            }

            Position = relativePositionParameters;

            int headingDegrees = relativePositionParameters.HeadingDegrees;
            var speed = (int) ((SpeedService.SpeedPercent*relativePositionParameters.AbsoluteDistance)/100.0);

            //SpheroTrace.Trace("Rolling to {0} with speed {1} - from params ({2:0.000},{3:0.000})", headingDegrees, speed, relativePositionParameters.X, relativePositionParameters.Y);
            var rollCommand = new RollCommand(speed, headingDegrees, false);
            SendCommand(rollCommand);
        }

        protected void DoStopRoll()
        {
            DoRoll(new RelativePositionParameters());
        }
    }
}