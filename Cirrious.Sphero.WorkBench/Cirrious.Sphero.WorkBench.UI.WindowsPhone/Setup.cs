// <copyright file="Setup.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using Microsoft.Phone.Controls;

namespace Cirrious.Sphero.WorkBench.UI.WindowsPhone
{
    public class Setup
        : MvxBaseWindowsPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame)
            : base(rootFrame)
        {
        }

        protected override void InitializeDefaultTextSerializer()
        {
            Cirrious.MvvmCross.Plugins.Json.PluginLoader.Instance.EnsureLoaded(true);
        }

        protected override MvxApplication CreateApp()
        {
            return new Cirrious.Sphero.WorkBench.Core.App();
        }

        protected override void AddPluginsLoaders(MvxLoaderPluginRegistry registry)
        {
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Accelerometer.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Color.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.File.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.ResourceLoader.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Settings.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Share.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Speech.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Sphero.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Visibility.WindowsPhone.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.XamPhotos.WindowsPhone.Plugin>();

            base.AddPluginsLoaders(registry);
        }

        protected override void InitializeLastChance()
        {
            Cirrious.MvvmCross.Plugins.Color.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded();

            base.InitializeLastChance();
        }
    }
}