// <copyright file="PluginLoader.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!





namespace Cirrious.MvvmCross.Plugins.XamPhotos
{
    public class PluginLoader
        : IMvxPluginLoader
          
    {
        public static readonly PluginLoader Instance = new PluginLoader();

        #region Implementation of IMvxPluginLoader

        public void EnsureLoaded()
        {
            var manager = Mvx.Resolve();
            manager.EnsureLoaded<PluginLoader>();
        }

        #endregion
    }
}