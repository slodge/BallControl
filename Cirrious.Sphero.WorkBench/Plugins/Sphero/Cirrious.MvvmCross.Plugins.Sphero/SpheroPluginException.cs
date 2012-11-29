// <copyright file="SpheroPluginException.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;

namespace Cirrious.MvvmCross.Plugins.Sphero
{
    public class SpheroPluginException : Exception
    {
        public SpheroPluginException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        public SpheroPluginException(Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }
}