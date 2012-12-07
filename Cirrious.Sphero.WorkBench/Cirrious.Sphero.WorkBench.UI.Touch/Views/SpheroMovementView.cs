
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Views;

namespace Cirrious.Sphero.WorkBench.UI.Touch.Views
{
	public partial class SpheroMovementView : MvxBindingTouchViewController<SpheroMovementViewModel>
	{
		public SpheroMovementView (MvxShowViewModelRequest request) : base (request, "SpheroMovementView", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.BackButton.TouchDown += (sender,e) => SendRoll(-1,0);
			this.LeftButton.TouchDown += (sender, e) => SendRoll(0,-1);
			this.RightButton.TouchDown += (sender, e) => SendRoll(0, 1);
			this.GoButton.TouchDown += (sender, e) => SendRoll(1,0);
			this.StopButton.TouchDown += (sender, e) => SendRoll(0,0);
		}

		private void SendRoll (double forwards, double right)
		{
			var position = new RelativePositionParameters()
			{
				X = forwards,
				Y = right
			};

			this.ViewModel.RollCommand.Execute(position);
		}

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

