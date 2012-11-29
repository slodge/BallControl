// <copyright file="SpheroReponseBuilderTest.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Sphero.Messages;
using NUnit.Framework;

namespace Cirrious.MvvmCross.Plugins.Sphero.Test.Responses
{
    [TestFixture]
    public class SpheroReponseBuilderTest
    {
        [TestCase]
        public void BuildsWithEmptyPayload()
        {
            var responseBuilder = new SpheroResponseBuilder();
            AssertOKButNotComplete(responseBuilder);

            responseBuilder.Add(0xff); // SOP1
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xff); // SOP2
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0x01); // Response
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0x31); // Sequence
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0x01); // Length of payload + checksum
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xcc); // inverted checksum
            AssertOKAndComplete(responseBuilder);
        }

        [TestCase]
        public void BuildsWithSimplePayload()
        {
            var responseBuilder = new SpheroResponseBuilder();
            AssertOKButNotComplete(responseBuilder);

            responseBuilder.Add(0xff); // SOP1
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xff); // SOP2
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xab); // Response
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0x21); // Sequence
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0x05); // Length of payload + checksum
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xa1); // payload
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xa2); // payload
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xa3); // payload
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xa4); // payload
            AssertOKButNotComplete(responseBuilder);
            responseBuilder.Add(0xa4); // inverted checksum
            AssertOKAndComplete(responseBuilder);
        }

        [TestCase]
        [ExpectedException(typeof (SpheroPluginException))]
        public void ThrowsExceptionWithInvalidCheckSum()
        {
            var responseBuilder = new SpheroResponseBuilder();

            responseBuilder.Add(0xff); // SOP1
            responseBuilder.Add(0xff); // SOP2
            responseBuilder.Add(0xab); // Response
            responseBuilder.Add(0x21); // Sequence
            responseBuilder.Add(0x05); // Length of payload + checksum
            responseBuilder.Add(0xa1); // payload
            responseBuilder.Add(0xa2); // payload
            responseBuilder.Add(0xa3); // payload
            responseBuilder.Add(0xa4); // payload

            // deliberately corrupt checksum
            responseBuilder.Add(0xd8); // inverted checksum
            Assert.Fail("We should throw an exception before here");
        }

        [TestCase]
        [ExpectedException(typeof (SpheroPluginException))]
        public void ThrowsExceptionWithInvalidHeader()
        {
            var responseBuilder = new SpheroResponseBuilder();

            responseBuilder.Add(0xfe); // SOP1
            Assert.Fail("We should throw an exception before here");
        }

        [TestCase]
        [ExpectedException(typeof (SpheroPluginException))]
        public void ThrowsExceptionWithInvalidHeader2()
        {
            var responseBuilder = new SpheroResponseBuilder();

            responseBuilder.Add(0xff); // SOP1
            responseBuilder.Add(0xfd); // SOP2
            Assert.Fail("We should throw an exception before here");
        }

        private static void AssertOKButNotComplete(SpheroResponseBuilder responseBuilder)
        {
            Assert.IsFalse(responseBuilder.IsComplete);
            Assert.IsFalse(responseBuilder.IsErrored);
        }

        private static void AssertOKAndComplete(SpheroResponseBuilder responseBuilder)
        {
            Assert.IsTrue(responseBuilder.IsComplete);
            Assert.IsFalse(responseBuilder.IsErrored);
        }
    }
}