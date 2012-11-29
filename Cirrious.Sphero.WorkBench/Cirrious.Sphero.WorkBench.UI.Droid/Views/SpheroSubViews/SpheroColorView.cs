// <copyright file="SpheroColorView.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Plugins.Color.Droid;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;

namespace Cirrious.Sphero.WorkBench.UI.Droid.Views.SpheroSubViews
{
    [Activity]
    public class SpheroColorView : MvxBindingActivityView<SpheroColorViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.ChildPage_SpheroColorView);

            // TODO - should do this color setting via databinding
            var view = FindViewById(Resource.Id.ShowCurrentColor);
            ViewModel.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == "Color")
                    {
                        view.SetBackgroundColor(ViewModel.Color.ToAndroidColor());
                    }
                };
        }
    }
}