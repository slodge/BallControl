// <copyright file="CartesianPositionParameters.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class CartesianPositionParameters
    {
        public double X { get; set; }
        public double Y { get; set; }

        public bool IsZero()
        {
            return X == 0.0 && Y == 0.0;
        }

        public double SimpleDistanceFrom(CartesianPositionParameters other)
        {
            return Math.Abs(other.X - this.X) + Math.Abs(other.Y - this.Y);
        }

        public int HeadingDegrees
        {
            get
            {
                var headingRadians = Math.Atan2(X, Y);
                var headingDegrees = (int) (headingRadians*180.0/Math.PI);
                while (headingDegrees < 0)
                    headingDegrees += 360;
                while (headingDegrees >= 360)
                    headingDegrees -= 360;

                return headingDegrees;
            }
        }

        public int AbsoluteDistance
        {
            get
            {
                var distance = RawCartesianDistance();
                if (distance > 1.0)
                    distance = 1.0;

                var speed = (int) (distance*255);

                return speed;
            }
        }

        public void ScaleToFullDistance()
        {
            var distance = RawCartesianDistance();
            if (distance == 0.0)
                return;

            var scale = 1.0/distance;
            X *= scale;
            Y *= scale;
        }

        private double RawCartesianDistance()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }
    }
}