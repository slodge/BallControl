using System;
using System.ComponentModel;
using Cirrious.MvvmCross.Dialog.Touch.Simple;
using CrossUI.Touch.Dialog.Elements;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Views;

namespace Cirrious.Sphero.WorkBench.UI.Touch.Views
{  
	public class SpheroColorView : MvxTouchDialogViewController<SpheroColorViewModel>
    {
        public SpheroColorView (MvxShowViewModelRequest request)
			: base(request)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
					
            this.Root = new RootElement("Sphero")
                            {
                                new Section("Enter Values")
                                    {
                                        Bind(
                                            new FloatElement(null, null, 0.0f)
                                                {
                                                    ShowCaption = false,
                                                    MinValue = 0.0f,
                                                    MaxValue = 255.0f
                                                },
                                            "{'Value':{'Path':'Red','Mode':'TwoWay'}}"),
										Bind(
											new FloatElement(null, null, 0.0f)
											{
											ShowCaption = false,
											MinValue = 0.0f,
											MaxValue = 255.0f
										},
										"{'Value':{'Path':'Green','Mode':'TwoWay'}}"),
										Bind(
											new FloatElement(null, null, 0.0f)
											{
											ShowCaption = false,
											MinValue = 0.0f,
											MaxValue = 255.0f
										},
										"{'Value':{'Path':'Blue','Mode':'TwoWay'}}"),
									}
                            };
        }
    }
}

