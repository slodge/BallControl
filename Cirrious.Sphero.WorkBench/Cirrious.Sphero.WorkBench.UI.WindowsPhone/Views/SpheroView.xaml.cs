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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Plugins.File;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;
using Cirrious.Sphero.WorkBench.UI.WindowsPhone.Controls;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using Windows.Phone.Media.Capture;
using Windows.Storage;

namespace Cirrious.Sphero.WorkBench.UI.WindowsPhone.Views
{
    public partial class SpheroView : BaseSpheroView
    {
        private AudioVideoCaptureDevice _cam;
        private bool _cameraIsRecording;
        private bool _cameraRecordingIsStartingOrStopping;
        private readonly DispatcherTimer _hackTimer; // should do a plugin for timer really!

        private bool CameraIsRecording
        {
            get { return _cameraIsRecording; }
            set
            {
                _cameraIsRecording = value;
                RecordingPanel.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public SpheroView()
        {
            InitializeComponent();
            _hackTimer = new DispatcherTimer();
            _hackTimer.Interval = TimeSpan.FromSeconds(1.0);
            _hackTimer.Tick += HackTimerOnTick;

            SetupMoveJoystick();
        }

        private void SetupMoveJoystick()
        {
            TheMoveJoystick.MouseLeftButtonDown += TheMoveJoystickOnMouseLeftButtonDown;
            TheMoveJoystick.MouseLeftButtonUp += TheMoveJoystickOnMouseLeftButtonUp;
            TheMoveJoystick.NewCoordinates += TheMoveJoystickOnNewCoordinates;
            TheMoveJoystick.Stop += TheMoveJoystickOnStop;
        }

        private void TheMoveJoystickOnStop(object sender, IsStoppedEventArgs isStoppedEventArgs)
        {
            if (isStoppedEventArgs.Stopped)
            {
                ThePivot.IsLocked = false;
                ViewModel.Movement.RollCommand.Execute(new CartesianPositionParameters());
            }
        }

        private void TheMoveJoystickOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ThePivot.IsLocked = false;
        }

        private void TheMoveJoystickOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ThePivot.IsLocked = true;
        }

        private void TheMoveJoystickOnNewCoordinates(object sender, MvxValueEventArgs<CartesianPositionParameters> eventArgs)
        {
            if (ViewModel != null)
            {
                ViewModel.Movement.RollCommand.Execute(eventArgs.Value);
            }
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
            RefreshSpecialPivots();
            Dispatcher.BeginInvoke(StartCameraAsync);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
                ViewModel.Shutdown();
        }

        private async void StartCameraAsync()
        {
            _cam = await AudioVideoCaptureDevice.OpenAsync(CameraSensorLocation.Back, AudioVideoCaptureDevice.GetAvailableCaptureResolutions(CameraSensorLocation.Back).First());
            TheVideoBrush.SetSource(_cam);
        }

        private void AccelMovementOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Position")
            {
                SetHightlightPosition(ViewModel.AccelMovement.Position, TheAccelHighlight, TheAccelParent);
            }
        }

        private bool _isNavigatingFrom;
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            _isNavigatingFrom = true;
            if (CameraIsRecording)
            {
/*
                var task = DoStopRecording();
                task.Start();
                task.Wait();
*/
            }
            TheMoveJoystick.StopJoystick();
            ViewModel.AccelMovement.EnsureAccelerometerIsOnCommand.Execute(false);
            ViewModel.Speech.EnsureSpeechIsOn.Execute(false);
            ViewModel.AccelMovement.PropertyChanged -= AccelMovementOnPropertyChanged;
            _hackTimer.Stop();
            if (_cam != null)
            {
                _cam.Dispose();
                _cam = null;
            }
            _isNavigatingFrom = false;
            base.OnNavigatingFrom(e);
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            // only one orientation for video!
            if (e.Orientation != PageOrientation.LandscapeLeft)
                return;

            // do not respond to orientation changes during pivot
            if (IsCurrentPivotItemAccelerometer())
                return;

            base.OnOrientationChanged(e);
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

        private void SetHightlightPosition(CartesianPositionParameters positionParameters, FrameworkElement highlightImage,
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
            positionParameters.ScaleToFullDistance();
            ViewModel.Heading.SetHeadingCommand.Execute(positionParameters.HeadingDegrees);
            SetHightlightPosition(positionParameters, TheHeadingHighlight, TheHeadingParent);
        }

        private CartesianPositionParameters GetRelativePositionParameters(object sender, MouseEventArgs e)
        {
            var target = (FrameworkElement) sender;
            var position = e.GetPosition(target);
            var relativeX = 2.0*(position.X/target.ActualWidth) - 1.0;
            var relativeY = 1.0 - 2.0*(position.Y/target.ActualHeight);
            var positionParameters = new CartesianPositionParameters {X = relativeX, Y = relativeY};
            return positionParameters;
        }

        private void ThePivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshSpecialPivots();
        }

        private void RefreshSpecialPivots()
        {
            if (IsCurrentPivotItemMovement())
            {
                TheMoveJoystick.StartJoystick();                
            }
            else
            {
                TheMoveJoystick.StopJoystick();
            }
            ViewModel.AccelMovement.EnsureAccelerometerIsOnCommand.Execute(IsCurrentPivotItemAccelerometer());
            ViewModel.Speech.EnsureSpeechIsOn.Execute(IsCurrentPivotItemSpeech());
        }


        private bool IsCurrentPivotItemAccelerometer()
        {
            return IsCurrentPivotItem("Accelerometer");
        }

        private bool IsCurrentPivotItemMovement()
        {
            return IsCurrentPivotItem("Move");
        }

        private bool IsCurrentPivotItemSpeech()
        {
            return IsCurrentPivotItem("Speech");
        }

        private bool IsCurrentPivotItem(string toTest)
        {
            if (ThePivot == null)
                return false;
            var currentTab = ThePivot.SelectedItem as PivotItem;
            if (currentTab == null)
                return false;
            return (string) currentTab.Tag == toTest;
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

/*
 * video disabled because.... because WP8 does not allow you to share videos... like wtf?
 * 
         private async void ApplicationBarIconButton_Video_OnClick(object sender, EventArgs e)
        {
            if (_cam == null)
            {
                return;
            }

            if (_cameraRecordingIsStartingOrStopping)
            {
                return;
            }
            
            _cameraRecordingIsStartingOrStopping = true;
            var button = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            if (CameraIsRecording)
            {
                // do stop
                CameraIsRecording = false;
                await DoStopRecording(true);
                button.IconUri = new Uri("/Assets/AppBar/appbar.video.png", UriKind.Relative);
            }
            else
            {
                // do start
                CameraIsRecording = true;
                await DoStartRecording();
                button.IconUri = new Uri("/Assets/AppBar/appbar.video.stop.png", UriKind.Relative);
            }
            _cameraRecordingIsStartingOrStopping = false;
        }

        private async Task DoStopRecording(bool navigate = false)
        {
            await _cam.StopRecordingAsync();
            CopyAndNavigate(navigate);
        }

        private void CopyAndNavigate(bool navigate)
        {
            var file = Mvx.Resolve<IMvxSimpleFileStoreService>();
            using (var ms = new MemoryStream())
            {
                file.TryReadBinaryFile(_videoPath, stream =>
                {
                    stream.CopyTo(ms);
                    return true;
                });
                ms.Seek(0, SeekOrigin.Begin);
                var lib = new MediaLibrary();
                var picture = lib.SavePicture(_videoPath, ms);
                file.DeleteFile(_videoPath);
                if (navigate)
                {
                    var task = new ShareMediaTask();
                    task.FilePath = picture.GetPath();
                    task.Show();
                }
            }
        }

        private string _videoPath;
        private async Task DoStartRecording()
        {
            StorageFolder isoStore = ApplicationData.Current.LocalFolder;
            _videoPath = string.Format("Sphero{0:yyyyMMddHHmmss}.wmv", DateTime.Now);
            var file = await isoStore.CreateFileAsync(_videoPath, CreationCollisionOption.ReplaceExisting);
            var s = await file.OpenAsync(FileAccessMode.ReadWrite);
            await _cam.StartRecordingToStreamAsync(s);
        }

*/ 
    }

    public abstract class BaseSpheroView : MvxPhonePage<SpheroViewModel>
    {
    }
}