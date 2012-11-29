// <copyright file="SetHeadingCommand.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

namespace Cirrious.MvvmCross.Plugins.Sphero.Commands
{
    public class SetHeadingCommand : BaseSpheroCommand
    {
        private readonly int _heading;

        public SetHeadingCommand(int heading)
            : base(DeviceSphero, CommandSetCal)
        {
            _heading = heading;
        }

        protected override byte[] GetPayloadBytes()
        {
            var data = new byte[2];
            data[0] = (byte) (_heading >> 8);
            data[1] = (byte) _heading;
            return data;
        }
    }
}