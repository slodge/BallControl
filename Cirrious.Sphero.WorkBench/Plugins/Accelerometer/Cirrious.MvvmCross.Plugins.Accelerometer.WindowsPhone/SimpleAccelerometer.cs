// <copyright file="SimpleAccelerometer.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Cirrious.MvvmCross.Exceptions;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Platform;
using Microsoft.Devices.Sensors;

namespace Cirrious.MvvmCross.Plugins.Accelerometer.WindowsPhone
{
    public class SimpleAccelerometer : ISimpleAccelerometer
    {
        private Microsoft.Devices.Sensors.Accelerometer _accelerometer;

        public void Start()
        {
            if (_accelerometer != null)
            {
                throw new MvxException("Accelerometer already started");
            }
            _accelerometer = new Microsoft.Devices.Sensors.Accelerometer();
            _accelerometer.CurrentValueChanged += AccelerometerOnCurrentValueChanged;
            _accelerometer.Start();
        }

        public void Stop()
        {
            if (_accelerometer == null)
            {
                throw new MvxException("Accelerometer not started");
            }
            _accelerometer.Stop();
            _accelerometer = null;
        }

        private void AccelerometerOnCurrentValueChanged(object sender,
                                                        SensorReadingEventArgs<AccelerometerReading>
                                                            sensorReadingEventArgs)
        {
            var handler = ReadingAvailable;

            if (handler == null)
                return;

            var reading = ToReading(sensorReadingEventArgs.SensorReading);

            handler(this, new MvxValueEventArgs<Reading>(reading));
        }

        private static Reading ToReading(AccelerometerReading sensorReading)
        {
            var reading = new Reading
                {
                    X = sensorReading.Acceleration.X,
                    Y = sensorReading.Acceleration.Y,
                    Z = sensorReading.Acceleration.Z,
                };
            return reading;
        }

        public bool Started
        {
            get { return _accelerometer != null; }
        }

        public Reading LastReading
        {
            get
            {
                try
                {
                    var reading = ToReading(_accelerometer.CurrentValue);
                    return reading;
                }
                catch (Exception exception)
                {
                    throw exception.MvxWrap("Problem getting current Accelerometer reading");
                }
            }
        }

        public event EventHandler<MvxValueEventArgs<Reading>> ReadingAvailable;
    }
}