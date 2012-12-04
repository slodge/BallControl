using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cirrious.Sphero.WorkBench.Core.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace Cirrious.Sphero.WorkBench.UI.WinRT.Views
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class HomeView : Cirrious.Sphero.WorkBench.UI.WinRT.Common.LayoutAwarePage
    {
        public HomeView()
        {
            this.InitializeComponent();
        }

        public new HomeViewModel ViewModel
        { 
            get { return base.ViewModel as HomeViewModel; }
            set { base.ViewModel = value; }
        }

        private void ItemListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectionChangedCommon(e);
        }

        private void ItemGridView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectionChangedCommon(e);
        }

        private void OnSelectionChangedCommon(SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count != 1)
                return;

            var toAdd = e.AddedItems.First();
            ViewModel.GoToSpheroCommand.Execute(toAdd);
        }
    }
}
