// <copyright file="BaseSpheroCommandTest.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Sphero.Commands;
using NUnit.Framework;

namespace Cirrious.MvvmCross.Plugins.Sphero.Test.Commands
{
    [TestFixture]
    public class BaseSpheroCommandTest
    {
        [TestCase]
        public void GeneratesValidCommandWithPayload()
        {
            var command = new ExampleCommand(0xde, 0xad, new byte[] {0x01});
            var packet = command.GetBytes(0x11);

            Assert.AreEqual(8, packet.Length);
            Assert.AreEqual(0xff, packet[0]); // SOP1
            Assert.AreEqual(0xff, packet[1]); // SOP2
            Assert.AreEqual(0xde, packet[2]); // Device
            Assert.AreEqual(0xad, packet[3]); // Command
            Assert.AreEqual(0x11, packet[4]); // Sequence
            Assert.AreEqual(0x02, packet[5]); // Length of payload + checksum
            Assert.AreEqual(0x01, packet[6]); // payload
            Assert.AreEqual(0x60, packet[7]); // inverted checksum
        }

        [TestCase]
        public void GeneratesValidCommandWithPayload2()
        {
            var command = new ExampleCommand(0xab, 0xcd, new byte[] {0xa1, 0xa2, 0xa3, 0xa4});
            var packet = command.GetBytes(0x21);

            Assert.AreEqual(11, packet.Length);
            Assert.AreEqual(0xff, packet[0]); // SOP1
            Assert.AreEqual(0xff, packet[1]); // SOP2
            Assert.AreEqual(0xab, packet[2]); // Device
            Assert.AreEqual(0xcd, packet[3]); // Command
            Assert.AreEqual(0x21, packet[4]); // Sequence
            Assert.AreEqual(0x05, packet[5]); // Length of payload + checksum
            Assert.AreEqual(0xa1, packet[6]); // payload
            Assert.AreEqual(0xa2, packet[7]); // payload
            Assert.AreEqual(0xa3, packet[8]); // payload
            Assert.AreEqual(0xa4, packet[9]); // payload
            Assert.AreEqual(0xd7, packet[10]); // inverted checksum
        }

        [TestCase]
        public void GeneratesValidCommandWithEmptyPayload()
        {
            var command = new ExampleCommand(0x01, 0x02, new byte[0]);
            var packet = command.GetBytes(0x31);

            Assert.AreEqual(7, packet.Length);
            Assert.AreEqual(0xff, packet[0]); // SOP1
            Assert.AreEqual(0xff, packet[1]); // SOP2
            Assert.AreEqual(0x01, packet[2]); // Device
            Assert.AreEqual(0x02, packet[3]); // Command
            Assert.AreEqual(0x31, packet[4]); // Sequence
            Assert.AreEqual(0x01, packet[5]); // Length of payload + checksum
            Assert.AreEqual(0xca, packet[6]); // inverted checksum
        }

        public class ExampleCommand : BaseSpheroCommand
        {
            private readonly byte[] _payload;

            public ExampleCommand(byte deviceId, byte commandId, byte[] payload)
                : base(deviceId, commandId)
            {
                _payload = payload;
            }

            protected override byte[] GetPayloadBytes()
            {
                return _payload;
            }
        }
    }
}