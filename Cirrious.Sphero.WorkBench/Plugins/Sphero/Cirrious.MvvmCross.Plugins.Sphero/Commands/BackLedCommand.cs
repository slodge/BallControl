// <copyright file="BackLedCommand.cs" company="Cirrious">
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
    public class BackLedCommand : BaseSpheroCommand
    {
        public BackLedCommand(int brightness)
            : base(DeviceSphero, CommandSetBackLed)
        {
            Brightness = brightness;
        }

        public int Brightness { get; set; }

        protected override sealed byte[] GetPayloadBytes()
        {
            if (Brightness > 255 || Brightness < 0)
                throw new ArgumentOutOfRangeException("Brightness", "Brightness must be between 255 and 0");

            var data = new byte[1];
            data[0] = (byte) Brightness;

            return data;
        }
    }
}