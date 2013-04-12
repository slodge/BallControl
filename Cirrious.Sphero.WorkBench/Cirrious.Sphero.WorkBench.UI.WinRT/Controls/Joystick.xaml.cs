using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Platform;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Cirrious.Sphero.WorkBench.UI.WinRT.Controls
{
    public sealed partial class Joystick : UserControl
    {
        public static readonly DependencyProperty TimerMilliSecondsProperty =
            DependencyProperty.Register("TimerMilliSeconds", typeof(double), typeof(Joystick),
                new PropertyMetadata(Convert.ToDouble(250), new PropertyChangedCallback(Joystick.OnTimerMilliSecondsChanged)));

        DispatcherTimer timer;

        int direction = 360;
        int speed = 0;
        int lastDirection = 0;
        int lastSpeed = 0;

        double lastX = 0;
        double lastY = 0;
        double newX = 0;
        double newY = 0;

        bool moveJoystick = false;

        public Joystick()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(TimerMilliSeconds);
            timer.Tick += timer_Tick;

            this.PointerPressed += OnPointerPressed;
            this.PointerReleased += OnPointerReleased;
        }


        public double TimerMilliSeconds
        {
            get
            {
                return Convert.ToDouble(GetValue(TimerMilliSecondsProperty));
            }
            set
            {
                SetValue(TimerMilliSecondsProperty, value);
            }
        }

        private static void OnTimerMilliSecondsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Joystick).timer.Interval = TimeSpan.FromMilliseconds((d as Joystick).TimerMilliSeconds);
        }

        private bool _started;
        public void StartJoystick()
        {
            if (_started)
                return;
            
            //Touch.FrameReported += Touch_FrameReported;
            this.PointerMoved += OnPointerMoved;
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            timer.Start();
            moveJoystick = true;
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            timer.Stop();

            // Fire event
            OnStop();

            // Move Joystick to Center
            MoveJoystick(0, 0);
            moveJoystick = false;
        }

        public void StopJoystick()
        {
            if (!_started)
                return;
            //Touch.FrameReported -= Touch_FrameReported;
            this.PointerMoved -= OnPointerMoved;
            _started = false;
        }

        private void OnPointerMoved(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            try
            {
                var point = pointerRoutedEventArgs.GetCurrentPoint(this);
                var p = point.Position;

                    // Update Shpero speed and direction
                    var center = new Point(this.ActualWidth / 2, this.ActualHeight / 2);

                    double distance = Math.Sqrt(Math.Pow((p.X - center.X), 2) + Math.Pow((p.Y - center.Y), 2));

                    var minHW = Math.Min(this.ActualHeight, this.ActualWidth);
                    double distanceRel = distance * 255 / (minHW / 2);
                    if (distanceRel > 255)
                    {
                        distanceRel = 255;
                    }

                    double angle = Math.Atan2(p.Y - center.Y, p.X - center.X) * 180 / Math.PI;
                    if (angle > 0)
                    {
                        angle += 90;
                    }
                    else
                    {
                        angle = 270 + (180 + angle);
                        if (angle >= 360)
                        {
                            angle -= 360;
                        }
                    }
                    direction = Convert.ToInt16(angle);
                    speed = Convert.ToInt16(distanceRel);

                    // Set Joystick Pos
                    newX = p.X - (this.ActualWidth / 2);
                    newY = p.Y - (this.ActualHeight / 2);
                    if (moveJoystick) MoveJoystick(newX, newY);
                
            }
            catch
            {
            }
        }

        /*
        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            try
            {
                int pointsNumber = e.GetTouchPoints(ellipseSense).Count;
                TouchPointCollection pointCollection = e.GetTouchPoints(ellipseSense);


                for (int i = 0; i < pointsNumber; i++)
                {
                    if (pointCollection[i].Position.X > 0 && pointCollection[i].Position.X < ellipseSense.ActualWidth)
                    {
                        if (pointCollection[i].Position.Y > 0 && pointCollection[i].Position.Y < ellipseSense.ActualHeight)
                        {
                            // Update Shpero speed and direction
                            Point p = pointCollection[i].Position;
                            Point center = new Point(ellipseSense.ActualWidth / 2, ellipseSense.ActualHeight / 2);

                            double distance = Math.Sqrt(Math.Pow((p.X - center.X), 2) + Math.Pow((p.Y - center.Y), 2));

                            double distanceRel = distance * 255 / (ellipseSense.ActualWidth / 2);
                            if (distanceRel > 255)
                            {
                                distanceRel = 255;
                            }

                            double angle = Math.Atan2(p.Y - center.Y, p.X - center.X) * 180 / Math.PI;
                            if (angle > 0)
                            {
                                angle += 90;
                            }
                            else
                            {
                                angle = 270 + (180 + angle);
                                if (angle >= 360)
                                {
                                    angle -= 360;
                                }
                            }
                            direction = Convert.ToInt16(angle);
                            speed = Convert.ToInt16(distanceRel);

                            // Set Joystick Pos
                            newX = p.X - (ellipseSense.ActualWidth / 2);
                            newY = p.Y - (ellipseSense.ActualWidth / 2);
                            if (moveJoystick) MoveJoystick(newX, newY);
                        }
                    }
                }
            }
            catch
            {
            }
        }
        */

        private void MoveJoystick(double moveX, double moveY)
        {
            Storyboard sb = new Storyboard();
            KeyTime ktStart = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
            KeyTime ktEnd = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));

            DoubleAnimationUsingKeyFrames animationFirstX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames animationFirstY = new DoubleAnimationUsingKeyFrames();

            ellipseButton.RenderTransform = new CompositeTransform();

            Storyboard.SetTargetProperty(animationFirstX, "TranslateX");
            Storyboard.SetTarget(animationFirstX, ellipseButton.RenderTransform);
            animationFirstX.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = lastX });
            animationFirstX.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = moveX });


            Storyboard.SetTargetProperty(animationFirstY, "TranslateY");
            Storyboard.SetTarget(animationFirstY, ellipseButton.RenderTransform);
            animationFirstY.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = lastY });
            animationFirstY.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = moveY });

            sb.Children.Add(animationFirstX);
            sb.Children.Add(animationFirstY);
            sb.Begin();

            lastX = moveX;
            lastY = moveY;
        }

        void timer_Tick(object sender, object e)
        {
            if (((direction - lastDirection) > 5 || (direction - lastDirection) < -5) || ((speed - lastSpeed) > 5 || (speed - lastSpeed) < -5))
            {
                lastDirection = direction;
                lastSpeed = speed;

                OnNewCoordinates();

                //Debug.WriteLine("Event fired: " + speed + ", " + direction);
            }
        }

        private void ellipseSense_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs manipulationStartedRoutedEventArgs)
        {
            //Debug.WriteLine("Manipulation Started");
        }

        private void ellipseSense_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs manipulationCompletedRoutedEventArgs)
        {
            //Debug.WriteLine("Manipulation Completed");
        }

        public event EventHandler<MvxValueEventArgs<CartesianPositionParameters>> NewCoordinates;

        protected void OnNewCoordinates()
        {
            var parameters = new CartesianPositionParameters()
            {
                X = newX / (ellipseSense.ActualWidth / 2),
                Y = newY / (ellipseSense.ActualWidth / 2),
            };
            var handler = NewCoordinates;
            if (handler != null)
                handler(this, new MvxValueEventArgs<CartesianPositionParameters>(parameters));
        }

        public event EventHandler<IsStoppedEventArgs> Stop;

        protected void OnStop()
        {
            var myStop = new IsStoppedEventArgs();
            myStop.Stopped = true;
            var handler = Stop;
            if (handler != null)
                handler(this, myStop);
        }
    }

    public class IsStoppedEventArgs : EventArgs
    {
        public bool Stopped;
    }
}
