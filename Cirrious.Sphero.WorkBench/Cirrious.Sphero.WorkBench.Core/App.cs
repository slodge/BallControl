// <copyright file="App.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.Sphero.WorkBench.Core.Interfaces;
using Cirrious.Sphero.WorkBench.Core.Services;
using Cirrious.Sphero.WorkBench.Core.ViewModels;

namespace Cirrious.Sphero.WorkBench.Core
{
    public class App
        : MvxApplication
    {
        public App()
        {
            Cirrious.MvvmCross.Plugins.File.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.ResourceLoader.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Sphero.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Share.PluginLoader.Instance.EnsureLoaded();

            // register the list service
            var spheroListService = new SpheroListService();
            Mvx.RegisterSingleton<ISpheroListService>(spheroListService);

            // register the speed service
            var spheroSpeedService = new SpheroSpeedService();
            Mvx.RegisterSingleton<ISpheroSpeedService>(spheroSpeedService);

            // set the start object
            RegisterAppStart<HomeViewModel>();
        }
    }
}