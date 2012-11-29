// <copyright file="RawMotorCommand.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

namespace Cirrious.MvvmCross.Plugins.Sphero.Commands
{
    public class RawMotorCommand : BaseSpheroCommand
    {
        public RawMotorCommand(RawMotorDirection leftMotorDirection, int leftMotorSpeed,
                               RawMotorDirection rightMotorDirection, int rightMotorSpeed)
            : base(DeviceSphero, CommandSetRawMotors)
        {
            LeftMotorDirection = leftMotorDirection;
            LeftMotorSpeed = leftMotorSpeed;
            RightMotorDirection = rightMotorDirection;
            RightMotorSpeed = rightMotorSpeed;
        }

        public RawMotorDirection LeftMotorDirection { get; set; }
        public int LeftMotorSpeed { get; set; }
        public RawMotorDirection RightMotorDirection { get; set; }
        public int RightMotorSpeed { get; set; }

        public enum RawMotorDirection
        {
            Forward = 1,
            Reverse = 2
        }

        protected override sealed byte[] GetPayloadBytes()
        {
            var data = new byte[4];

            data[0] = (byte) (int) this.LeftMotorDirection;
            data[1] = (byte) this.LeftMotorSpeed;
            data[2] = (byte) (int) this.RightMotorDirection;
            data[3] = (byte) this.RightMotorSpeed;

            return data;
        }
    }
}