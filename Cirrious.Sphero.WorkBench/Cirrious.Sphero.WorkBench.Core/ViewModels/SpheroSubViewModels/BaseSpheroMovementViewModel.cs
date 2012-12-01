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

        private CartesianPositionParameters _position;

        protected BaseSpheroMovementViewModel(ISpheroParentViewModel parent) : base(parent)
        {
            Position = new CartesianPositionParameters();
        }

        public CartesianPositionParameters Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged(() => Position);
            }
        }

        protected void DoRoll(CartesianPositionParameters positionParameters)
        {
            // TODO - put the check back in for 'too close'
            //if (Position.AbsoluteDistance < DistanceThreshold)
            //{
            //    return;
            //}

            Position = positionParameters;

            int headingDegrees = positionParameters.HeadingDegrees;
            var speed = (int) ((SpeedService.SpeedPercent*positionParameters.AbsoluteDistance)/100.0);

            SpheroTrace.Trace("Rolling to {0} with speed {1})", headingDegrees, speed);
            var rollCommand = new RollCommand(speed, headingDegrees, false);
            SendCommand(rollCommand);
        }

        protected void DoStopRoll()
        {
            DoRoll(new CartesianPositionParameters());
        }
    }
}