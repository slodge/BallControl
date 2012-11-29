// <copyright file="SpheroException.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;

namespace Cirrious.Sphero.WorkBench.Core
{
    public class SpheroException : Exception
    {
        public SpheroException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        public SpheroException(Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }
}