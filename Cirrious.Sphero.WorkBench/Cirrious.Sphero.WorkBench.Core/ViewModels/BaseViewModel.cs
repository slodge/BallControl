// <copyright file="BaseViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Settings;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        public ICommand GoToPhotoListCommand
        {
            get { return new MvxCommand(DoGoToPhotoList); }
        }


        public ICommand GoToBluetoothCommand
        {
            get { return new MvxCommand(DoGoToBluetoothSettings); }
        }

        private void DoGoToPhotoList()
        {
            this.ShowViewModel<PhotoListViewModel>();
        }

        private void DoGoToBluetoothSettings()
        {
            Cirrious.MvvmCross.Plugins.Settings.PluginLoader.Instance.EnsureLoaded();
            var settings = Mvx.Resolve<ISettings>();
            if (settings.CanShow(KnownSettings.Bluetooth))
            {
                settings.Show(KnownSettings.Bluetooth);
            }
        }
    }
}