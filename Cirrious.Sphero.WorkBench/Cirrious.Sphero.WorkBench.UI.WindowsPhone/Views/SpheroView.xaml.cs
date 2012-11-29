// <copyright file="SpheroView.xaml.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;
using Microsoft.Devices;
using Microsoft.Phone.Controls;

namespace Cirrious.Sphero.WorkBench.UI.WindowsPhone.Views
{
    public partial class SpheroView : BaseSpheroView
    {
        private PhotoCamera _cam;
        private readonly DispatcherTimer _hackTimer;

        public SpheroView()
        {
            InitializeComponent();
            _hackTimer = new DispatcherTimer();
            _hackTimer.Interval = TimeSpan.FromSeconds(1.0);
            _hackTimer.Tick += HackTimerOnTick;
        }

        private void HackTimerOnTick(object sender, EventArgs eventArgs)
        {
            if (ViewModel != null)
            {
                ViewModel.CheckConnectionCommand.Execute(null);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.AccelMovement.PropertyChanged += AccelMovementOnPropertyChanged;
            _hackTimer.Start();
            //_cam = new PhotoCamera();
            //TheVideoBrush.SetSource(_cam);
        }

        private void AccelMovementOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Position")
            {
                SetHightlightPosition(ViewModel.AccelMovement.Position, TheAccelHighlight, TheAccelParent);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.AccelMovement.EnsureAccelerometerIsOnCommand.Execute(false);
            ViewModel.Speech.EnsureSpeechIsOn.Execute(false);
            ViewModel.AccelMovement.PropertyChanged += AccelMovementOnPropertyChanged;
            _hackTimer.Stop();
            if (_cam != null)
            {
                _cam.Dispose();
                _cam = null;
            }
            base.OnNavigatingFrom(e);
        }

        private void Target_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ThePivot.IsLocked = true;
            e.Handled = true;
            ExecuteRoll(sender, e);
            TheMoveHighlight.Visibility = Visibility.Visible;
        }

        private void Target_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TheMoveHighlight.Visibility = Visibility.Collapsed;
            ThePivot.IsLocked = false;
            e.Handled = true;
            // send a stop rolling command
            ViewModel.Movement.RollCommand.Execute(new RelativePositionParameters());
        }

        private void Target_OnMouseMove(object sender, MouseEventArgs e)
        {
            ExecuteRoll(sender, e);
        }

        private void Heading_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ThePivot.IsLocked = true;
            e.Handled = true;
            ViewModel.Heading.ZeroRelativeHeadingCommand.Execute(null);
            ExecuteHeading(sender, e);
            TheHeadingHighlight.Visibility = Visibility.Visible;
        }

        private void Heading_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TheHeadingHighlight.Visibility = Visibility.Collapsed;
            ThePivot.IsLocked = false;
            e.Handled = true;
            ExecuteHeading(sender, e);
        }

        private void Heading_OnMouseMove(object sender, MouseEventArgs e)
        {
            ExecuteHeading(sender, e);
        }

        private void ExecuteRoll(object sender, MouseEventArgs e)
        {
            var positionParameters = GetRelativePositionParameters(sender, e);
            ViewModel.Movement.RollCommand.Execute(positionParameters);
            SetHightlightPosition(positionParameters, TheMoveHighlight, TheMoveParent);
        }

        private void SetHightlightPosition(RelativePositionParameters positionParameters, Image highlightImage,
                                           Canvas theParent)
        {
            Canvas.SetTop(highlightImage,
                          (theParent.ActualHeight - positionParameters.Y*theParent.ActualHeight -
                           highlightImage.ActualHeight)/2);
            Canvas.SetLeft(highlightImage,
                           (theParent.ActualWidth + positionParameters.X*theParent.ActualWidth -
                            highlightImage.ActualWidth)/2);
        }

        private void ExecuteHeading(object sender, MouseEventArgs e)
        {
            var positionParameters = GetRelativePositionParameters(sender, e);
            ViewModel.Heading.SetHeadingCommand.Execute(positionParameters.HeadingDegrees);
            SetHightlightPosition(positionParameters, TheHeadingHighlight, TheHeadingParent);
        }

        private RelativePositionParameters GetRelativePositionParameters(object sender, MouseEventArgs e)
        {
            var target = (FrameworkElement) sender;
            var position = e.GetPosition(target);
            var relativeX = 2.0*(position.X/target.ActualWidth) - 1.0;
            var relativeY = 1.0 - 2.0*(position.Y/target.ActualHeight);
            var positionParameters = new RelativePositionParameters {X = relativeX, Y = relativeY};
            return positionParameters;
        }

        private void ThePivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshSpecialPivots();
        }

        private void RefreshSpecialPivots()
        {
            ViewModel.AccelMovement.EnsureAccelerometerIsOnCommand.Execute(IsCurrentPivotItemAccelerometer());
            ViewModel.Speech.EnsureSpeechIsOn.Execute(IsCurrentPivotItemSpeech());
        }


        private bool IsCurrentPivotItemAccelerometer()
        {
            var currentTab = ThePivot.SelectedItem as PivotItem;
            if (currentTab == null)
                return false;
            return (string) currentTab.Tag == "Accelerometer";
        }

        private bool IsCurrentPivotItemSpeech()
        {
            var currentTab = ThePivot.SelectedItem as PivotItem;
            if (currentTab == null)
                return false;
            return (string) currentTab.Tag == "Speech";
        }

        private void ApplicationBarIconButton_Photo_OnClick(object sender, EventArgs e)
        {
            this.ViewModel.TakePhotoCommand.Execute(null);
            //if (_cam == null)
            //{
            //    _cam = new PhotoCamera();
            //    TheVideoBrush.SetSource(_cam);
            //}
            //else
            //{
            //    _cam.Dispose();
            //    _cam = null;
            //}
        }

        private void ApplicationBarIconButton_Bluetooth_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToBluetoothCommand.Execute(null);
        }

        private void ApplicationBarIconButton_Photos_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToPhotoListCommand.Execute(null);
        }
    }

    public class BaseSpheroView : MvxPhonePage<SpheroViewModel>
    {
    }
}