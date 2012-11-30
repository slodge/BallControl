using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.WinRT.Platform;
using Windows.UI.Xaml.Controls;

namespace Cirrious.Sphero.WorkBench.UI.WinRT
{
    public class Setup
        : MvxBaseWinRTSetup
    {
        public Setup(Frame rootFrame)
            : base(rootFrame)
        {
        }

        protected override MvxApplication CreateApp()
        {
            var app = new Core.App();
            return app;
        }

        protected override void AddPluginsLoaders(Cirrious.MvvmCross.Platform.MvxLoaderPluginRegistry registry)
        {
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Accelerometer.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Color.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.File.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.ResourceLoader.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Settings.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Share.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Speech.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Sphero.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Visibility.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.WebBrowser.WinRT.Plugin>();
            registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.XamPhotos.WinRT.Plugin>();


            base.AddPluginsLoaders(registry);
        }
    }
}
