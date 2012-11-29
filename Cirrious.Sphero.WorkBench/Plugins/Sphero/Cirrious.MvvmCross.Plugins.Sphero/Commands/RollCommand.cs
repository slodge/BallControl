// <copyright file="RollCommand.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;

namespace Cirrious.MvvmCross.Plugins.Sphero.Commands
{
    public class RollCommand : BaseSpheroCommand
    {
        public RollCommand(int velocity, int heading, bool isStop)
            : base(DeviceSphero, CommandRoll)
        {
            Velocity = velocity;
            Heading = heading;
            IsStop = isStop;
        }

        public int Velocity { get; set; }

        public int Heading { get; set; }

        public bool IsStop { get; set; }

        protected override sealed byte[] GetPayloadBytes()
        {
            if (Velocity < 0 || Velocity > 255)
                throw new ArgumentOutOfRangeException("Velocity must be between 0-255");
            if (Heading < 0 || Heading > 359)
                throw new ArgumentOutOfRangeException("Heading must be between 0-359");

            var data = new byte[4];

            data[0] = (byte) (this.Velocity);
            data[1] = (byte) ((this.Heading >> 8));
            data[2] = (byte) this.Heading;
            data[3] = (byte) (this.IsStop ? 0 : 1);

            return data;
        }
    }
}