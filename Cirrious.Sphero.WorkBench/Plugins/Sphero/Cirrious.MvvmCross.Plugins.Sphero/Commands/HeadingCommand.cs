// <copyright file="HeadingCommand.cs" company="Cirrious">
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
    public class HeadingCommand : BaseSpheroCommand
    {
        public HeadingCommand(int heading)
            : base(DeviceSphero, CommandSetCal)
        {
            Heading = heading;
        }

        public int Heading { get; set; }


        protected override sealed byte[] GetPayloadBytes()
        {
            if (Heading < 0 || Heading > 359)
                throw new ArgumentOutOfRangeException("Heading", "Heading must be between 1-359");

            var data = new byte[2];
            data[0] = (byte) (this.Heading >> 8);
            data[1] = (byte) this.Heading;

            return data;
        }
    }
}