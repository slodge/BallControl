// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Cirrious.Sphero.WorkBench.UI.Touch.Views
{
	[Register ("SpheroMovementView")]
	partial class SpheroMovementView
	{
		[Outlet]
		MonoTouch.UIKit.UIButton GoButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton LeftButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton RightButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton StopButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton BackButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (GoButton != null) {
				GoButton.Dispose ();
				GoButton = null;
			}

			if (LeftButton != null) {
				LeftButton.Dispose ();
				LeftButton = null;
			}

			if (RightButton != null) {
				RightButton.Dispose ();
				RightButton = null;
			}

			if (StopButton != null) {
				StopButton.Dispose ();
				StopButton = null;
			}

			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}
		}
	}
}
