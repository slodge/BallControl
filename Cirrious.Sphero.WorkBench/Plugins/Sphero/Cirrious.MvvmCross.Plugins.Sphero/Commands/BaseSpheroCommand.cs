// <copyright file="BaseSpheroCommand.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.Plugins.Sphero.Messages;

namespace Cirrious.MvvmCross.Plugins.Sphero.Commands
{
    public abstract class BaseSpheroCommand : ISpheroCommand
    {
        public const int
            DeviceCore = 0,
            DeviceBootloader = 1,
            DeviceSphero = 2;

        public const int CommandSetCal = 0x01;
        public const int CommandSetStabiliz = 0x02;
        public const int CommandSetRotationRate = 0x03;
        public const int CommandSetBallRegWebsite = 0x04;
        public const int CommandGetBallRegWebsite = 0x05;
        public const int CommandReenableDemo = 0x06;
        public const int CommandGetChassisId = 0x07;
        public const int CommandSetChassisId = 0x08;
        public const int CommandSelfLevel = 0x09;
        public const int CommandSetDataStreaming = 0x11;
        public const int CommandSetCollisionDet = 0x12;
        public const int CommandSetRGBLed = 0x20;
        public const int CommandSetBackLed = 0x21;
        public const int CommandGetRGBLed = 0x22;
        public const int CommandRoll = 0x30;
        public const int CommandBoost = 0x31;
        public const int CommandMove = 0x32;
        public const int CommandSetRawMotors = 0x33;
        public const int CommandSetMotionTo = 0x34;
        public const int CommandSetOptionsFlag = 0x35;
        public const int CommandGetOptionsFlag = 0x36;
        public const int CommandGetConfigBlk = 0x40;
        public const int CommandSetDeviceMode = 0x42;
        public const int CommandSetCfgBlock = 0x43;
        public const int CommandGetDeviceMode = 0x44;
        public const int CommandRunMacro = 0x44;
        public const int CommandSaveTempMacro = 0x51;
        public const int CommandSaveMacro = 0x52;
        public const int CommandInitMacroExecutive = 0x54;
        public const int CommandAbortMacro = 0x55;
        public const int CommandMacroStatus = 0x56;
        public const int CommandSetMacroParam = 0x56;
        public const int CommandAppendTempMacroChunk = 0x58;
        public const int CommandEraseOrbbas = 0x60;
        public const int CommandAppendFrag = 0x61;
        public const int CommandExecOrbbas = 0x62;
        public const int CommandAbortOrbbas = 0x63;

        private const byte CommandPrefix = 255;

        private const int
            ChecksumLength = 1,
            IndexStart1 = 0,
            IndexStart2 = 1,
            IndexDeviceId = 2,
            IndexCommand = 3,
            IndexCommandSequenceNo = 4,
            IndexCommandDataLength = 5,
            CommandHeaderLength = 6;

        protected BaseSpheroCommand(byte deviceId, byte commandId)
        {
            DeviceId = deviceId;
            CommandId = commandId;
        }

        public byte[] GetBytes(int sequenceNumber)
        {
            var data = GetPayloadBytes();

            var dataLength = data != null ? data.Length : 0;
            var packetLength = dataLength + CommandHeaderLength + ChecksumLength;

            var buffer = new byte[packetLength];
            var checksum = (byte) 0;

            buffer[IndexStart1] = CommandPrefix;
            buffer[IndexStart2] = CommandPrefix;

            var deviceId = DeviceId;
            checksum = (byte) (checksum + deviceId);
            buffer[IndexDeviceId] = deviceId;

            var command = CommandId;
            checksum = (byte) (checksum + command);
            buffer[IndexCommand] = command;

            checksum = (byte) (checksum + sequenceNumber);
            buffer[IndexCommandSequenceNo] = (byte) (sequenceNumber);

            var responseLength = (byte) (dataLength + 1);
            checksum = (byte) (checksum + responseLength);
            buffer[IndexCommandDataLength] = responseLength;

            // Check if we need to calculate the checksum for the data we have added
            if (data != null)
            {
                // Calculate the checksum for the data (also add the data to the array)
                for (var i = 0; i < dataLength; i++)
                {
                    buffer[(i + CommandHeaderLength)] = data[i];
                    checksum = (byte) (checksum + data[i]);
                }
            }

            buffer[(packetLength - ChecksumLength)] = (byte) (checksum ^ 0xFFFFFFFF);

            return buffer;
        }

        protected abstract byte[] GetPayloadBytes();

        public virtual ISpheroMessage ProcessResponse(SpheroResponse response)
        {
            // default behaviour is SimpleResponse 
            // - any valid response packet is a good response.
            // - and returned message is empty
            return null;
        }

        public byte DeviceId { get; private set; }

        public byte CommandId { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}({1},{2})", this.GetType().Name, DeviceId, CommandId);
        }
    }
}