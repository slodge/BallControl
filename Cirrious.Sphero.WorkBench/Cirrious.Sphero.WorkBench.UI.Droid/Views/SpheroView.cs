// <copyright file="SpheroView.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;

namespace Cirrious.Sphero.WorkBench.UI.Droid.Views
{
    [Activity(Label = "Ball Control", ScreenOrientation = ScreenOrientation.Landscape)]
    public class SpheroView : MvxBindingTabActivityView<SpheroViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_SpheroView);

            TabHost.TabSpec spec; // Resusable TabSpec for each tab
            Intent intent; // Reusable Intent for each tab

            // Initialize a TabSpec for each tab and add it to the TabHost
            spec = TabHost.NewTabSpec("move");
            spec.SetIndicator("Move", Resources.GetDrawable(Resource.Drawable.Tab_Move));
            spec.SetContent(CreateIntentFor(ViewModel.Movement));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("turn");
            spec.SetIndicator("Turn", Resources.GetDrawable(Resource.Drawable.Tab_Turn));
            spec.SetContent(CreateIntentFor(ViewModel.Heading));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("color");
            spec.SetIndicator("Color", Resources.GetDrawable(Resource.Drawable.Tab_Color));
            spec.SetContent(CreateIntentFor(ViewModel.Color));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("accel");
            spec.SetIndicator("Tilt", Resources.GetDrawable(Resource.Drawable.Tab_MoveAccel));
            spec.SetContent(CreateIntentFor(ViewModel.AccelMovement));
            TabHost.AddTab(spec);

            TabHost.TabChanged += (sender, args) =>
                {
                    var isAccelTab = (args.TabId == "accel");
                    ViewModel.AccelMovement.EnsureAccelerometerIsOnCommand.Execute(isAccelTab);
                };
        }

        protected override void OnPause()
        {
            ViewModel.AccelMovement.EnsureAccelerometerIsOnCommand.Execute(false);
            base.OnPause();
        }
    }
}