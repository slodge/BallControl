// <copyright file="Plugin.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!


using Cirrious.CrossCore.Plugins;

namespace Cirrious.MvvmCross.Plugins.Speech.WinRT
{
    public class Plugin
        : IMvxPlugin
          
    {
        public void Load()
        {
            // sadly speech is not available in Metro style apps
            // e.g see http://social.msdn.microsoft.com/Forums/en-US/winappswithcsharp/thread/968d1f79-1f26-4eb7-88c2-5fa5c7877d4b
            //Mvx.RegisterType<ISpeechListener, SpeechListener>();
        }
    }
}