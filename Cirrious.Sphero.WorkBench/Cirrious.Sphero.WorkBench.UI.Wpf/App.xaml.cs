using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;


namespace Cirrious.Sphero.WorkBench.UI.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var start = Mvx.Resolve<IMvxAppStart>();
            start.Start();
        }
    }
}
