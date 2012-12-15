using System.Windows.Threading;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Wpf.Interfaces;
using Cirrious.MvvmCross.Wpf.Platform;

namespace Cirrious.Sphero.WorkBench.UI.Wpf
{
    public class Setup
        : MvxBaseWpfSetup
    {
        public Setup(Dispatcher dispatcher, IMvxWpfViewPresenter presenter)
            : base(dispatcher, presenter)
        {
        }

        protected override MvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeDefaultTextSerializer()
        {
            Cirrious.MvvmCross.Plugins.Json.PluginLoader.Instance.EnsureLoaded(true);
        }

        protected override void InitializeLastChance()
        {
            Cirrious.MvvmCross.Plugins.Color.PluginLoader.Instance.EnsureLoaded();
            base.InitializeLastChance();
        }
    }
}
