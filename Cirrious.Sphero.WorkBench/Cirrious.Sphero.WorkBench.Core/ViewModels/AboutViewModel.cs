// <copyright file="AboutViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.WebBrowser;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ICommand GoToUrlCommand
        {
            get { return new MvxCommand<string>(DoGoToUrl);}
        }

        private void DoGoToUrl(string url)
        {
            Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader.Instance.EnsureLoaded();
            var browser = Mvx.Resolve<IMvxWebBrowserTask>();
            browser.ShowWebPage(url);
        }
    }
}