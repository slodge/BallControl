// <copyright file="SpheroResponse.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Collections.Generic;

namespace Cirrious.MvvmCross.Plugins.Sphero.Messages
{
    public class SpheroResponse
    {
        public byte StartOfPacket1 { get; set; }
        public byte StartOfPacket2 { get; set; }
        public byte SequenceNumber { get; set; }
        public byte MessageResponse { get; set; }
        public byte DataLength { get; set; }
        public List<byte> Payload { get; private set; }
        public byte CheckSum { get; set; }

        public SpheroResponse()
        {
            Payload = new List<byte>();
        }

        public bool IsStreamingResponse
        {
            get { return StartOfPacket2 == 0xFE; }
        }

        public bool Validate()
        {
            if (StartOfPacket1 != 0xFF)
                return false;
            if (StartOfPacket2 != 0xFF && StartOfPacket2 != 0xFE)
                return false;
            if (DataLength != (Payload.Count + 1))
                return false;

            var calculated = 0x00;
            calculated += MessageResponse;
            calculated += SequenceNumber;
            calculated += DataLength;
            foreach (var x in Payload)
            {
                calculated += x;
            }

            var inverted = (byte) (calculated ^ 0xFFFFFFFF);
            return inverted == CheckSum;
        }
    }
}