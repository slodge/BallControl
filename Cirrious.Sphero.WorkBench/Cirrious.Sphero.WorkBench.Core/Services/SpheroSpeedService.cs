// <copyright file="SpheroSpeedService.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.ViewModels;
using Cirrious.Sphero.WorkBench.Core.Interfaces;

namespace Cirrious.Sphero.WorkBench.Core.Services
{
    public class SpheroSpeedService
        : MvxNotifyPropertyChanged
          , ISpheroSpeedService
    {
        private const double DefaultSpeedFactor = 50.0;

        private double _speedPercent = DefaultSpeedFactor;

        public double SpeedPercent
        {
            get { return _speedPercent; }
            set
            {
                _speedPercent = value;
                RaisePropertyChanged(() => SpeedPercent);
            }
        }
    }
}