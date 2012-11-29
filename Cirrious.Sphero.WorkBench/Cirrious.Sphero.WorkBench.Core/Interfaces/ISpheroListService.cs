// <copyright file="ISpheroListService.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Collections.Generic;
using System.ComponentModel;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;

namespace Cirrious.Sphero.WorkBench.Core.Interfaces
{
    public interface ISpheroColorService : INotifyPropertyChanged
    {
        void RefreshList();
        bool IsRefreshing { get; }
        IList<IAvailableSphero> AvailableSpheros { get; }
    }

    public interface ISpheroListService : INotifyPropertyChanged
    {
        void RefreshList();
        bool IsRefreshing { get; }
        IList<IAvailableSphero> AvailableSpheros { get; }
    }
}