// <copyright file="SpheroListService.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.Sphero.WorkBench.Core.Interfaces;

namespace Cirrious.Sphero.WorkBench.Core.Services
{
    public class SpheroListService
        : MvxNotifyPropertyChanged
          , ISpheroListService
    {
        private bool _isRefreshing;
        private IList<IAvailableSphero> _availableSpheros;

        public SpheroListService()
        {
            RefreshList();
        }

        public void RefreshList()
        {
            if (IsRefreshing)
                return;
            lock (this)
            {
                if (IsRefreshing)
                    return;

                IsRefreshing = true;
            }

            var finder = this.GetService<ISpheroFinder>();
            finder.Find(OnFindSuccess, OnFindError);
        }

        private void OnFindError(Exception error)
        {
            // TODO - report the error
            IsRefreshing = false;
        }

        private void OnFindSuccess(IList<IAvailableSphero> spheros)
        {
            AvailableSpheros = spheros;
            IsRefreshing = false;
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            private set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        public IList<IAvailableSphero> AvailableSpheros
        {
            get { return _availableSpheros; }
            private set
            {
                _availableSpheros = value;
                RaisePropertyChanged(() => AvailableSpheros);
            }
        }
    }
}