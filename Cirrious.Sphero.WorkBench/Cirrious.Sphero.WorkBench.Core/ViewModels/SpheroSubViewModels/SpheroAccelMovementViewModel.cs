// <copyright file="SpheroAccelMovementViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Plugins.Accelerometer;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class SpheroAccelMovementViewModel : BaseSpheroMovementViewModel
    {
        private ISimpleAccelerometer _accelerometer;

        public SpheroAccelMovementViewModel(ISpheroParentViewModel parent)
            : base(parent)
        {
            Cirrious.MvvmCross.Plugins.Accelerometer.PluginLoader.Instance.EnsureLoaded();
        }

        public ICommand EnsureAccelerometerIsOnCommand
        {
            get { return new MvxRelayCommand<bool>(DoEnsureAccelerometerIsOn); }
        }

        private void DoEnsureAccelerometerIsOn(bool requestedState)
        {
            var accelerometerIsOn = _accelerometer != null;
            if (accelerometerIsOn == requestedState)
                return;

            if (requestedState)
            {
                _accelerometer = this.GetService<ISimpleAccelerometer>();
                _accelerometer.ReadingAvailable += AccelerometerOnReadingAvailable;
                _accelerometer.Start();
            }
            else
            {
                _accelerometer.Stop();
                _accelerometer.ReadingAvailable -= AccelerometerOnReadingAvailable;
                _accelerometer = null;
                DoStopRoll();
            }
        }

        private void AccelerometerOnReadingAvailable(object sender, MvxValueEventArgs<Reading> mvxValueEventArgs)
        {
            // note that the switchover between Y and X here is deliberate!
            // and that we invert Y
            // this may need tweaking platform-by-platform 
            // and phone by phone - and maybe at runtime too!

            var parameters = new CartesianPositionParameters
                {
                    X = MakeSafe(-mvxValueEventArgs.Value.Y),
                    Y = MakeSafe(mvxValueEventArgs.Value.X),
                };

            DoRoll(parameters);
        }

        private double MakeSafe(double d, double min = -1.0, double max = 1.0)
        {
            if (d < min)
                return min;
            if (d > max)
                return max;
            return d;
        }
    }
}