// <copyright file="SimpleAccelerometer.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Cirrious.MvvmCross.Touch.Interfaces;
using Cirrious.MvvmCross.Exceptions;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Platform;
using MonoTouch.UIKit;

namespace Cirrious.MvvmCross.Plugins.Accelerometer.Touch
{
    public class SimpleAccelerometer
        :  ISimpleAccelerometer
          , IMvxServiceConsumer
    {
		bool _initialised = false;

        public void Start()
        {
			if (_initialised)
            {
                throw new MvxException("Accelerometer already started");
            }

			_initialised = true;

			UIAccelerometer.SharedAccelerometer.UpdateInterval = 0.1;
			
			UIAccelerometer.SharedAccelerometer.Acceleration += HandleAccelerationChange;
	    }

        void HandleAccelerationChange (object sender, UIAccelerometerEventArgs e)
        {
			var reading = ToReading(e.Acceleration.X, e.Acceleration.Y, e.Acceleration.Z);
			
			LastReading = reading.Clone();
			
			var handler = ReadingAvailable;
			
			if (handler == null)
				return;
			
			handler(this, new MvxValueEventArgs<Reading>(reading));
		}

        public void Stop()
        {
            if (!_initialised)
            {
                throw new MvxException("Accelerometer not started");
            }

			_initialised = false;
			UIAccelerometer.SharedAccelerometer.Acceleration -= HandleAccelerationChange;
        }

        private static Reading ToReading(double x, double y, double z)
        {
            var reading = new Reading
                {
                    X = x,
                    Y = y,
                    Z = z,
                };
            return reading;
        }

        public bool Started
        {
            get { return _initialised; }
        }

        public Reading LastReading { get; private set; }

        public event EventHandler<MvxValueEventArgs<Reading>> ReadingAvailable;
    }
}