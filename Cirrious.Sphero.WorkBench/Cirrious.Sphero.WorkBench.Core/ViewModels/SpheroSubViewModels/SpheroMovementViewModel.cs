// <copyright file="SpheroMovementViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class SpheroMovementViewModel : BaseSpheroMovementViewModel
    {
        public SpheroMovementViewModel(ISpheroParentViewModel parent)
            : base(parent)
        {
        }

        public ICommand RollCommand
        {
            get { return new MvxCommand<CartesianPositionParameters>(DoRoll); }
        }


        /*
        public int SpinSpeed = 128;
        public int RollSpeed = 128;

        public ICommand LeftCommand
        {
            get { return new MvxCommand(DoSpinLeft); }
        }

        public ICommand RightCommand
        {
            get { return new MvxCommand(DoSpinRight); }
        }

        public ICommand StopCommand
        {
            get { return new MvxCommand(DoStop); }
        }

        public ICommand ForwardsCommand
        {
            get { return new MvxCommand(DoForwards); }
        }

        public ICommand BackwardsCommand
        {
            get { return new MvxCommand(DoBackwards); }
        }


        private void DoForwards()
        {
            var startCommand = new RollCommand(RollSpeed, 0, false);
            SendCommand(startCommand);
        }

        private void DoBackwards()
        {
            var startCommand = new RollCommand(RollSpeed, 180, false);
            SendCommand(startCommand);
        }

        private void DoSpinLeft()
        {
            var startCommand = new SpinLeftCommand(SpinSpeed);
            SendCommand(startCommand);
        }

        private void DoSpinRight()
        {
            var startCommand = new SpinRightCommand(SpinSpeed);
            SendCommand(startCommand);
        }

        private void DoStop()
        {
            var stopCommand = new StopCommand();
            SendCommand(stopCommand);
            //var stabalization = new SetStabilizationCommand(true);
            //SendCommand(stopCommand);
        }
         */
    }
}