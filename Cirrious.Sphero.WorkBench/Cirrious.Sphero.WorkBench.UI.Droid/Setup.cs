// <copyright file="Setup.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;
using Android.Content;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.Plugins.Color;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.Sphero.WorkBench.Core;

namespace Cirrious.Sphero.WorkBench.UI.Droid
{
    public class Setup
        : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        public class Converters
        {
            public readonly MvxNativeColorValueConverter SimpleColor = new MvxNativeColorValueConverter();
            //public readonly MvxVisibilityConverter Visibility = new MvxVisibilityConverter();
        }

        protected override List<Type> ValueConverterHolders
        {
            get { return new List<Type>() {typeof (Converters)}; }
        }

        protected override IDictionary<string, string> ViewNamespaceAbbreviations
        {
            get
            {
                var toReturn = base.ViewNamespaceAbbreviations;
                toReturn["SpheroApp"] = "Cirrious.Sphero.WorkBench.UI.Droid.Controls";
                return toReturn;
            }
        }

        protected override void InitializeLastChance()
        {
            //var errorHandler = new ErrorDisplayer(ApplicationContext);
            //Cirrious.MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded();
            //Cirrious.CrossCore.UI.PluginLoader.Instance.EnsureLoaded();
            base.InitializeLastChance();
        }
    }
}