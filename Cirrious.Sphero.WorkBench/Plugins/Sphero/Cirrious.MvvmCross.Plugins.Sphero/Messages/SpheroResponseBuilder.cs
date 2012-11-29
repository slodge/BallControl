// <copyright file="SpheroResponseBuilder.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;

namespace Cirrious.MvvmCross.Plugins.Sphero.Messages
{
    public class SpheroResponseBuilder
    {
        public enum State
        {
            WaitingForSop1,
            WaitingForSop2,
            WaitingForMrsp,
            WaitingForSeq,
            WaitingForDlen,
            WaitingForPayloadAndCheckSum,
            Complete,
            Error = -1,
            ErrorHeader = -2,
            ErrorCheckSum = -3,
            ErrorOverflow = -4,
        }

        private State _currentState;

        public State CurrentState
        {
            get { return _currentState; }
        }

        public bool IsComplete
        {
            get { return _currentState == State.Complete; }
        }

        public bool IsErrored
        {
            get { return _currentState < 0; }
        }

        private readonly SpheroResponse _response;

        public SpheroResponse Response
        {
            get { return _response; }
        }

        public SpheroResponseBuilder()
        {
            _response = new SpheroResponse();
            _currentState = State.WaitingForSop1;
        }

        public void Add(byte x)
        {
            if (IsErrored)
                throw new SpheroPluginException("Response already errored. You cannot add more bytes to it.");

            switch (_currentState)
            {
                case State.WaitingForSop1:
                    if (x != 0xff)
                    {
                        _currentState = State.ErrorHeader;
                        throw new SpheroPluginException("Unexpected packet SOP1 header byte {0}", x);
                    }
                    _response.StartOfPacket1 = x;
                    _currentState++;
                    break;
                case State.WaitingForSop2:
                    if (x != 0xff && x != 0xfe)
                    {
                        _currentState = State.ErrorHeader;
                        throw new SpheroPluginException("Unexpected packet SOP2 header byte {0}", x);
                    }
                    _response.StartOfPacket2 = x;
                    _currentState++;
                    break;
                case State.WaitingForMrsp:
                    _response.MessageResponse = x;
                    _currentState++;
                    break;
                case State.WaitingForSeq:
                    _response.SequenceNumber = x;
                    _currentState++;
                    break;
                case State.WaitingForDlen:
                    _response.DataLength = x;
                    _currentState++;
                    break;
                case State.WaitingForPayloadAndCheckSum:
                    if (_response.DataLength - _response.Payload.Count > 1)
                    {
                        _response.Payload.Add(x);
                    }
                    else
                    {
                        _response.CheckSum = x;
                        if (!_response.Validate())
                        {
                            _currentState = State.ErrorCheckSum;
                            throw new SpheroPluginException("CheckSum error in received packet");
                        }
                        _currentState++;
                    }
                    break;
                case State.Complete:
                    _currentState = State.ErrorOverflow;
                    throw new SpheroPluginException("You cannot add any more bytes to this complete response");
                case State.Error:
                case State.ErrorHeader:
                case State.ErrorCheckSum:
                case State.ErrorOverflow:
                    // do nothing
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}