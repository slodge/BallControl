// <copyright file="Plugin.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Plugins;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;

namespace Cirrious.MvvmCross.Plugins.Speech.WindowsPhone
{
    public class Plugin
        : IMvxPlugin
          , IMvxServiceProducer
    {
        #region Implementation of IMvxPlugin

        public void Load()
        {
            this.RegisterServiceType<ISpeechListener, SpeechListener>();
        }

        #endregion
    }
}