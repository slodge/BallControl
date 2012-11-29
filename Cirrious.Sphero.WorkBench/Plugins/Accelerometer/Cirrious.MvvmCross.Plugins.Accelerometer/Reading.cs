// <copyright file="Reading.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

namespace Cirrious.MvvmCross.Plugins.Accelerometer
{
    public class Reading
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Reading Clone()
        {
            return new Reading {X = X, Y = Y, Z = Z};
        }
    }
}