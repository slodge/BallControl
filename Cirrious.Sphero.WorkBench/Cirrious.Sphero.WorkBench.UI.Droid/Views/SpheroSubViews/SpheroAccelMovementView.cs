// <copyright file="SpheroAccelMovementView.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;

namespace Cirrious.Sphero.WorkBench.UI.Droid.Views.SpheroSubViews
{
    [Activity]
    public class SpheroAccelMovementView : MvxBindingActivityView<SpheroAccelMovementViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.ChildPage_SpheroAccelMovementView);
        }
    }
}