// <copyright file="AboutView.xaml.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.WindowsPhone.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;

namespace Cirrious.Sphero.WorkBench.UI.WindowsPhone.Views
{
    public partial class AboutView : BaseAboutView
    {
        public AboutView()
        {
            InitializeComponent();
        }
    }

    public class BaseAboutView : MvxPhonePage<AboutViewModel>
    {
    }
}