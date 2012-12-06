using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.Binding.Interfaces;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;

namespace Cirrious.Sphero.WorkBench.UI.Touch.Views
{
    public partial class HomeView
		: MvxBindingTouchTableViewController<HomeViewModel>
    {
        public HomeView (MvxShowViewModelRequest request) 
            : base (request)
        {
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
			Title = "Ball Control";
			
			var source = new MvxActionBasedBindableTableViewSource(
				TableView,
				UITableViewCellStyle.Subtitle,
				new NSString("SpheroBalls"),
				"{'TitleText':{'Path':'Name'},'SelectedCommand':{'Path':'GoToSpheroCommand'}}",
				UITableViewCellAccessory.DisclosureIndicator);
			
			source.CellModifier = (cell) =>
			{
				cell.Image.DefaultImagePath = "icon.png";
			};
			
			this.AddBindings(
				new Dictionary<object, string>()
				{
					{ source, "{'ItemsSource':{'Path':'ListService.AvailableSpheros'}}" }
				});
			
			TableView.Source = source;
			TableView.ReloadData();
			
			NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, e) => ViewModel.RefreshListCommand.Execute(null)), false);	
        }
    }
}

