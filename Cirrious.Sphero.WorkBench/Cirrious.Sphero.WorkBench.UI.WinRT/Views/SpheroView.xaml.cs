using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Platform;
using Cirrious.Sphero.WorkBench.Core.ViewModels;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;
using Cirrious.Sphero.WorkBench.UI.WinRT.Controls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Cirrious.Sphero.WorkBench.UI.WinRT.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SpheroView : Cirrious.Sphero.WorkBench.UI.WinRT.Common.LayoutAwarePage
    {
        public SpheroView()
        {
            this.InitializeComponent();
            MoveJoystick.NewCoordinates += MoveJoystickOnNewCoordinates;
            MoveJoystick.Stop += MoveJoystickOnStop;
        }

        public new SpheroViewModel ViewModel
        {
            get { return base.ViewModel as SpheroViewModel; }
            set { base.ViewModel = value;  }
        }

        private void MoveJoystickOnStop(object sender, IsStoppedEventArgs isStoppedEventArgs)
        {
            if (isStoppedEventArgs.Stopped)
                ViewModel.Movement.RollCommand.Execute(new CartesianPositionParameters());
        }

        private void MoveJoystickOnNewCoordinates(object sender, MvxValueEventArgs<CartesianPositionParameters> eventArgs)
        {
            if (ViewModel != null)
            {
                ViewModel.Movement.RollCommand.Execute(eventArgs.Value);
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshVisibilities();
        }

        private void RefreshVisibilities()
        {
            var grids = new Grid[]
                {
                    MoveGrid,
                    TurnGrid,
                    ColorGrid,
                    TiltGrid,
                };

            var selectedIndex = 0;
            if (SelectView != null)
                selectedIndex = SelectView.SelectedIndex;
            for (var i = 0; i < grids.Length; i++)
            {
                if (grids[i] != null)
                {
                    grids[i].Visibility = i == selectedIndex ? Visibility.Visible : Visibility.Collapsed;
                }   
            }

            if (ViewModel != null)
                ViewModel.AccelMovement.EnsureAccelerometerIsOnCommand.Execute(selectedIndex == 3);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MoveJoystick.StartJoystick();
            RefreshVisibilities();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            MoveJoystick.StopJoystick();
            ViewModel.AccelMovement.EnsureAccelerometerIsOnCommand.Execute(false);
        }
    }
}
