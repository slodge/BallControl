using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Touch.Interfaces;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Platform;

namespace Cirrious.Sphero.WorkBench.UI.Touch
{
    public class Setup
        : MvxTouchDialogBindingSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxTouchViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        #region Overrides of MvxBaseSetup

        protected override MvxApplication CreateApp()
        {
            var app = new Core.App();
            return app;
        }

        protected override IEnumerable<Type> ValueConverterHolders {
			get { return new Type[0]; } // { typeof(Converters) }; }
        }

		protected override void AddPluginsLoaders(MvxLoaderPluginRegistry registry)
		{
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Accelerometer.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Color.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.DownloadCache.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.File.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.ResourceLoader.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Settings.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Share.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Speech.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Sphero.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Visibility.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.WebBrowser.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.XamPhotos.Touch.Plugin>();
			base.AddPluginsLoaders(registry);
		}
        #endregion
    }
}
