// <copyright file="HomeView.xaml.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
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

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (TheListBox.SelectedIndex >= 0)
            {
                TheListBox.SelectedIndex = -1;
            }
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
    }

    public class BaseHomeView : MvxPhonePage<HomeViewModel>
    {
    }
}