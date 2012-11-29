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
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Binding.Droid;
using Cirrious.MvvmCross.Plugins.Color;
using Cirrious.Sphero.WorkBench.Core;

namespace Cirrious.Sphero.WorkBench.UI.Droid
{
    public class Setup
        : MvxBaseAndroidBindingSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override MvxApplication CreateApp()
        {
            return new App();
        }

        public class Converters
        {
            public readonly MvxSimpleColorConverter SimpleColor = new MvxSimpleColorConverter();
            //public readonly MvxVisibilityConverter Visibility = new MvxVisibilityConverter();
        }

        protected override IEnumerable<Type> ValueConverterHolders
        {
            get { return new[] {typeof (Converters)}; }
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
            Cirrious.MvvmCross.Plugins.Color.PluginLoader.Instance.EnsureLoaded();
            base.InitializeLastChance();
        }
    }
}