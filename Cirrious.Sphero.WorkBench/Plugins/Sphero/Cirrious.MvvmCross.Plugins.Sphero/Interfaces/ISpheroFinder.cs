// <copyright file="ISpheroFinder.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;

namespace Cirrious.MvvmCross.Plugins.Sphero.Interfaces
{
    public interface ISpheroFinder
    {
        void Find(Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError);
    }
}