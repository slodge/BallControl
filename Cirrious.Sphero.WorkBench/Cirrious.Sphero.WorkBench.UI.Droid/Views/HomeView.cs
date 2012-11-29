// <copyright file="HomeView.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;

namespace Cirrious.Sphero.WorkBench.UI.Droid.Views
{
    [Activity(Label = "Ball Control")]
    public class HomeView : MvxBindingActivityView<HomeViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_HomeView);
        }
    }
}