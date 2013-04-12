// <copyright file="Plugin.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!





namespace Cirrious.MvvmCross.Plugins.XamPhotos.Droid
{
    public class Plugin
        : IMvxPlugin
          
    {
        #region Implementation of IMvxPlugin

        public void Load()
        {
            Mvx.RegisterSingleton<IPhotoPicker>(new PhotoPicker());
        }

        #endregion
    }
}