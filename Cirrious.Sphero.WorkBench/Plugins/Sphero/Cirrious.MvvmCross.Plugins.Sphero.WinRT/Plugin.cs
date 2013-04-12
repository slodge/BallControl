// <copyright file="Plugin.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!


using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.Plugins.Sphero.WinRT.Tooth;

namespace Cirrious.MvvmCross.Plugins.Sphero.WinRT
{
    public class Plugin
        : IMvxPlugin
          
    {
        public void Load()
        {
            Mvx.RegisterSingleton<ISpheroFinder>(new SpheroFinder());
        }
  }
}