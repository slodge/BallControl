// <copyright file="IAccelerometer.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Cirrious.MvvmCross.Platform;

namespace Cirrious.MvvmCross.Plugins.Accelerometer
{
    public interface ISimpleAccelerometer
    {
        void Start();
        void Stop();
        bool Started { get; }
        Reading LastReading { get; }
        event EventHandler<MvxValueEventArgs<Reading>> ReadingAvailable;
    }
}