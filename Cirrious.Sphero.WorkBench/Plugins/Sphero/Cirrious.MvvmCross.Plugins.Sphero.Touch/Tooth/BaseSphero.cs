// <copyright file="BaseSphero.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using MonoTouch.ExternalAccessory;

namespace Cirrious.MvvmCross.Plugins.Sphero.Touch.Tooth
{
    public class BaseSphero : IBaseSphero
    {
        private readonly EAAccessory _accessory;

		protected EAAccessory Accessory
        {
            get { return _accessory; }
        }

        protected BaseSphero(EAAccessory accessory)
        {
            _accessory = accessory;
        }

        public string Name
        {
            get { return _accessory.Name; }
        }
    }
}