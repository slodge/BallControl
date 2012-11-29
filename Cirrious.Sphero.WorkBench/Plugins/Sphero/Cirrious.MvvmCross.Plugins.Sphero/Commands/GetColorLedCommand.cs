// <copyright file="GetColorLedCommand.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Color;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.Plugins.Sphero.Messages;

namespace Cirrious.MvvmCross.Plugins.Sphero.Commands
{
    public class GetColorLedCommand : BaseSpheroCommand
    {
        public class ColorMessage : ISpheroMessage
        {
            private readonly MvxColor _color;

            public MvxColor Color
            {
                get { return _color; }
            }

            public ColorMessage(int r, int g, int b)
            {
                _color = new MvxColor(r, g, b);
            }
        }

        public GetColorLedCommand()
            : base(DeviceSphero, CommandGetRGBLed)
        {
        }

        protected override sealed byte[] GetPayloadBytes()
        {
            return null;
        }

        public override ISpheroMessage ProcessResponse(SpheroResponse response)
        {
            if (response.Payload.Count != 3)
            {
                // TODO - report error...
                return null;
            }

            return new ColorMessage(response.Payload[0], response.Payload[1], response.Payload[2]);
        }
    }
}