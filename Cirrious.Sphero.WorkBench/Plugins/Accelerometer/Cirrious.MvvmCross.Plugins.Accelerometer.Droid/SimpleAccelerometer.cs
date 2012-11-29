// <copyright file="SimpleAccelerometer.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Android.Content;
using Android.Hardware;
using Cirrious.MvvmCross.Droid.Interfaces;
using Cirrious.MvvmCross.Exceptions;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Platform;

namespace Cirrious.MvvmCross.Plugins.Accelerometer.Droid
{
    public class SimpleAccelerometer
        : Java.Lang.Object
          , ISensorEventListener
          , ISimpleAccelerometer
          , IMvxServiceConsumer
    {
        private Sensor _accelerometer;
        private SensorManager _sensorManager;

        public void Start()
        {
            if (_accelerometer != null)
            {
                throw new MvxException("Accelerometer already started");
            }

            var globals = this.GetService<IMvxAndroidGlobals>();
            _sensorManager = (SensorManager) globals.ApplicationContext.GetSystemService(Context.SensorService);
            if (_sensorManager == null)
                throw new MvxException("Failed to find SensorManager");

            _accelerometer = _sensorManager.GetDefaultSensor(SensorType.Accelerometer);
            if (_accelerometer == null)
                throw new MvxException("Failed to find Accelerometer");

            // It is not necessary to get accelerometer events at a very high
            // rate, by using a slower rate (SENSOR_DELAY_UI), we get an
            // automatic low-pass filter, which "extracts" the gravity component
            // of the acceleration. As an added benefit, we use less power and
            // CPU resources.
            _sensorManager.RegisterListener(this, _accelerometer, SensorDelay.Ui);
        }

        public void Stop()
        {
            if (_accelerometer == null)
            {
                throw new MvxException("Accelerometer not started");
            }
            _sensorManager.UnregisterListener(this);
            _sensorManager = null;
            _accelerometer = null;
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // ignored
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type != SensorType.Accelerometer)
                return;

            var reading = ToReading(e.Values[0], e.Values[1], e.Values[2]);

            LastReading = reading.Clone();

            var handler = ReadingAvailable;

            if (handler == null)
                return;

            handler(this, new MvxValueEventArgs<Reading>(reading));
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
            get { return _accelerometer != null; }
        }

        public Reading LastReading { get; private set; }

        public event EventHandler<MvxValueEventArgs<Reading>> ReadingAvailable;
    }
}