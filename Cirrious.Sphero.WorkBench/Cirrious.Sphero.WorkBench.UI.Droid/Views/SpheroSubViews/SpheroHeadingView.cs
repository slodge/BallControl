// <copyright file="SpheroHeadingView.cs" company="Cirrious">
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
using Cirrious.Sphero.WorkBench.UI.Droid.Controls;

namespace Cirrious.Sphero.WorkBench.UI.Droid.Views.SpheroSubViews
{
    [Activity]
    public class SpheroHeadingView : MvxBindingActivityView<SpheroHeadingViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.ChildPage_SpheroHeadingView);

            var touch = this.FindViewById<MovementTrackingTouchView>(Resource.Id.TouchView);

            touch.TouchStart += (sender, args) => ViewModel.ZeroRelativeHeadingCommand.Execute(null);
            touch.TouchPositionChanged +=
                (sender, args) => ViewModel.SetHeadingCommand.Execute(touch.TouchPosition.HeadingDegrees);
        }
    }
}