// <copyright file="HomeView.xaml.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Sphero.Commands;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;
using Windows.System.Threading;

namespace Cirrious.Sphero.WorkBench.UI.WindowsPhone.Views
{
    public partial class GangnamStyleView
        : BaseGangnamStyleView
    {
        public GangnamStyleView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private bool played;
        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsConnected")
            {
                if (played)
                    return;

                played = true;
                var task = Dance();
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
                ViewModel.Shutdown();
        }

        private void ApplicationBarIconButton_Bluetooth_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToBluetoothCommand.Execute(null);
        }

        private async void MediaElement_OnMediaOpened(object sender, RoutedEventArgs e)
        {
        }

        private async Task Dance()
        {
            Stop();
            Color(0,0,0);
            Tail(0);
            await TailTo100Over10();
            await OneSecond();
            PulseWhiteEverySecond(6);
            Color(0,200,0);
            for (var i = 0; i < 5; i++ )
                await ZigZag();

            Debug.WriteLine("Starting... 2");
            for (var i = 0; i < 3; i++)
            {
                await Hack2();
                /*
                var zigZag = ZigZag();
                var pulse = PulseWhiteOnce();
                Task.WaitAll(new Task[] {zigZag, pulse});
                 */
            }
            Debug.WriteLine("Starting... 3");

            for (var i = 0; i < 3; i++)
            {
                await Hack3();
                /*
                var tail = TailTo100Over10();
                //var zigZag = ZigZag();
                var zigRoll = ZigRoll();
                var pulse = LongerPulseRandomColor();
                Task.WaitAll(new Task[] { tail, zigRoll, pulse });
                 */
            }

            Debug.WriteLine("Ending... 2");
            for (var i = 0; i < 20; i++)
            {
                await Hack3();
                await Hack2();
                Color(0, 0, 0);
                for (var j = 0; j < 5; j++)
                    await ZigZag();
                /*
                var tail = TailTo100Over10();
                //var zigZag = ZigZag();
                var zigRoll = ZigRoll();
                var pulse = LongerPulseRandomColor();
                Task.WaitAll(new Task[] { tail, zigRoll, pulse });
                 */
            }
        }

        private async Task Hack2()
        {
            var r = new Random();

            Color(r.Next(255), r.Next(255), r.Next(255));
            await WaitSeconds(1.0);
            Turn(90);
            Color();
            await WaitSeconds(0.5);
            Color(r.Next(255), r.Next(255), r.Next(255));
            await WaitSeconds(1.0);
            Turn(270);
            await WaitSeconds(0.5);
            Color(r.Next(255), r.Next(255), r.Next(255));
        }

        private async Task Hack3()
        {
            var r = new Random();

            Color(r.Next(255), r.Next(255), r.Next(255));
            Roll(100, 90.0);
            await WaitSeconds(1.0);
            Roll();
            await WaitSeconds(0.2);
            Color(r.Next(255), r.Next(255), r.Next(255));
            await WaitSeconds(0.2);
            Roll(100, 180);
            await WaitSeconds(1.0);
            Roll();
            await WaitSeconds(0.2);
            Color(r.Next(255), r.Next(255), r.Next(255));
            await WaitSeconds(0.2);
            Roll(100, 270);
            await WaitSeconds(1.0);
            Roll();
            await WaitSeconds(0.2);
            Color(r.Next(255), r.Next(255), r.Next(255));
            await WaitSeconds(0.2);
            Color(r.Next(255), r.Next(255), r.Next(255));
            Roll(100, 0);
            await WaitSeconds(1.0);
            Color(r.Next(255), r.Next(255), r.Next(255));
            Roll();
            await WaitSeconds(0.5);
            Color();
        }

        private async Task LongerPulseRandomColor()
        {
            var r = new Random();

            Color(r.Next(255), r.Next(255), r.Next(255));
            await WaitSeconds(1.0);
            Color();
            await WaitSeconds(0.5);
            Color(r.Next(255), r.Next(255), r.Next(255));
            await WaitSeconds(1.0);
        }

        private async Task ZigZag()
        {
            Turn(90);
            await OneSecond();
            Turn(270);
            await OneSecond();
        }

        private async Task ZigRoll()
        {
            Roll(100, 90.0);
            await WaitSeconds(1.0);
            Roll(100, 180);
            await WaitSeconds(1.0);
            Roll(100, 270);
            await WaitSeconds(1.0);
            Roll(100, 0);
            await WaitSeconds(1.0);
            Roll();
            await WaitSeconds(1.0);
        }

        private async void PulseWhiteEverySecond(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await PulseWhiteOnce();
            }
        }

        private async Task PulseWhiteOnce()
        {
            Color(255, 255, 255);
            await WaitSeconds(0.2);
            Color();
            await WaitSeconds(0.8);
        }

        private async Task TailTo100Over10()
        {
            for (int i = 0; i < 10; i++)
            {
                Tail(i * 10);
                await OneSecond();
            }
            Tail(100);
        }

        private void Color(int r=0, int g=0, int b=0)
        {
            Send(new SetColorLedCommand(new MvxColor(r,g,b)));
        }

        private void Tail(int brightness)
        {
            Send(new BackLedCommand(brightness));
        }

        private void Turn(int degress)
        {
            Send(new HeadingCommand(degress));
        }

        private static Task WaitSeconds(double seconds)
        {
            return Task.Delay(TimeSpan.FromSeconds(seconds));
        }

        private static Task OneSecond()
        {
            return Task.Delay(1000);
        }

        private void Roll(double speed=0, double heading=0)
        {
            Send(new RollCommand((int)speed, (int)heading, false));
        }

        private void Stop()
        {
            Send(new RollCommand(0, 0, false));
        }

        private void Send(ISpheroCommand spheroCommand)
        {
            ViewModel.SendCommand(spheroCommand);
        }
    }

    public abstract class BaseGangnamStyleView : MvxPhonePage<GangnamStyleViewModel>
    {
    }
}