// <copyright file="PhotoListView.xaml.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;

namespace Cirrious.Sphero.WorkBench.UI.WindowsPhone.Views
{
    public partial class PhotoListView : BasePhotoListView
    {
        public PhotoListView()
        {
            InitializeComponent();
        }

        private void MenuItem_Share_Click(object sender, RoutedEventArgs e)
        {
            var pwc = GetPhotoWithCommands(sender);
            pwc.ShareCommand.Execute(null);
        }

        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            var pwc = GetPhotoWithCommands(sender);
            pwc.DeleteCommand.Execute(null);
        }

        private static PhotoListViewModel.PhotoWithCommands GetPhotoWithCommands(object sender)
        {
            var uiElement = sender as FrameworkElement;
            var pwc = uiElement.Tag as PhotoListViewModel.PhotoWithCommands;
            return pwc;
        }
    }

    public abstract class BasePhotoListView : MvxPhonePage<PhotoListViewModel>
    {
    }
}