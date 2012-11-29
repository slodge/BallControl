// <copyright file="SpheroTrace.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Platform.Diagnostics;

namespace Cirrious.Sphero.WorkBench.Core
{
    public static class SpheroTrace
    {
        public const string Tag = "Cirrious.SpheroApp";

        public static void Trace(string message, params object[] args)
        {
            MvxTrace.TaggedTrace(Tag, message, args);
        }
    }
}