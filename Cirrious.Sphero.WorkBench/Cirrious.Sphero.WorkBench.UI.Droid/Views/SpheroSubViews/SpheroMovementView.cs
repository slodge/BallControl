// <copyright file="SpheroMovementView.cs" company="Cirrious">
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
    public class SpheroMovementView : MvxBindingActivityView<SpheroMovementViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.ChildPage_SpheroMovementView);

            var touch = this.FindViewById<MovementTrackingTouchView>(Resource.Id.TouchView);
            touch.TouchEnd += (sender, args) => ViewModel.RollCommand.Execute(new CartesianPositionParameters());
            touch.TouchPositionChanged += (sender, args) => ViewModel.RollCommand.Execute(touch.TouchPosition);
        }
    }
}