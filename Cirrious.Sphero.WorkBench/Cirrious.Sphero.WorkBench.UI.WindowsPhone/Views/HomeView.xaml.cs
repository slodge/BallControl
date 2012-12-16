// <copyright file="HomeView.xaml.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Windows;
using System.Windows.Navigation;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;

namespace Cirrious.Sphero.WorkBench.UI.WindowsPhone.Views
{
    public partial class HomeView
        : BaseHomeView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private bool _hackVisibleConnected;
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (TheListBox.SelectedIndex >= 0)
            {
                TheListBox.SelectedIndex = -1;
            }

            /// HACK HACK - this should be bound and should use NotifyCollectionChanged to
            if (_hackVisibleConnected)
                return;

            if (e.NavigationMode == NavigationMode.Back)
                this.ViewModel.RefreshListCommand.Execute(null);

            HackVisibleHelp();
            ViewModel.ListService.PropertyChanged += (sender, args) => HackVisibleHelp();
            _hackVisibleConnected = true;
        }

        private void HackVisibleHelp()
        {
            var visible = (ViewModel.ListService.AvailableSpheros == null
                           || ViewModel.ListService.AvailableSpheros.Count == 0);
            NoSpheroText.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ApplicationBarMenuItem_About_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToAboutCommand.Execute(null);
        }

        private void ApplicationBarIconButton_Photos_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToPhotoListCommand.Execute(null);
        }

        private void ApplicationBarIconButton_Refresh_OnClick(object sender, EventArgs e)
        {
            ViewModel.RefreshListCommand.Execute(null);
        }

        private void ApplicationBarIconButton_Bluetooth_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToBluetoothCommand.Execute(null);
        }

        private void ApplicationBarMenuItem_Gangnam_OnClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("OK to play music now?", "Ball Control", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            ViewModel.GoToGangnamStyleCommand.Execute(null);
        }
    }

    public class BaseHomeView : MvxPhonePage<HomeViewModel>
    {
    }
}