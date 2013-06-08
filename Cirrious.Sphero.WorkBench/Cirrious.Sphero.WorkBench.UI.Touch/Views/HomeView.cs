using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;

namespace Cirrious.Sphero.WorkBench.UI.Touch.Views
{
    public class HomeView
		: MvxTableViewController
    {
        public new HomeViewModel ViewModel
        {
            get { return base.ViewModel as HomeViewModel; }
            set { base.ViewModel = value; }
        }
        public HomeView() 
        {
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
			Title = "Ball Control";
			
			var source = new MvxActionBasedTableViewSource(
				TableView,
				UITableViewCellStyle.Subtitle,
				new NSString("SpheroBalls"),
				"TitleText Name",
				UITableViewCellAccessory.DisclosureIndicator);
			
			source.CellModifier = (cell) =>
			{
				cell.ImageLoader.DefaultImagePath = "Images/SpheroIcon100.png";
			};

			source.SelectionChangedCommand = ViewModel.GoToSpheroCommand;
			this.AddBindings(
				new Dictionary<object, string>()
				{
				{ source, "ItemsSource ListService.AvailableSpheros" }
				});
			
			TableView.Source = source;
			TableView.ReloadData();

			var button = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, e) => {
				ViewModel.RefreshListCommand.Execute(null);
			});
			NavigationItem.SetRightBarButtonItem(button, false);	
        }
    }
}

