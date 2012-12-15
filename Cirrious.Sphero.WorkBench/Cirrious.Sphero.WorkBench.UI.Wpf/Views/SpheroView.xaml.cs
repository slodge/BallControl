using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cirrious.MvvmCross.Wpf.Views;
using Cirrious.Sphero.WorkBench.Core.ViewModels;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;

namespace Cirrious.Sphero.WorkBench.UI.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SpheroView.xaml
    /// </summary>
    public partial class SpheroView : MvxWpfView
    {
        public SpheroView()
        {
            InitializeComponent();
        }

        public new SpheroViewModel ViewModel
        {
            get { return base.ViewModel as SpheroViewModel; }
            set { base.ViewModel = value; }
        }

        private void ButtonOnClickForwards(object sender, RoutedEventArgs e)
        {
            DoMove(0, 1);
        }

        private void ButtonOnClickReverse(object sender, RoutedEventArgs e)
        {
            DoMove(0, -1);
        }

        private void ButtonOnClickLeft(object sender, RoutedEventArgs e)
        {
            DoMove(1, 0);
        }

        private void ButtonOnClickRight(object sender, RoutedEventArgs e)
        {
            DoMove(-1, 0);
        }

        private void ButtonOnClickStop(object sender, RoutedEventArgs e)
        {
            DoMove();
        }

        private void DoMove(double x=0, double y=0)
        {
            var parameters = new CartesianPositionParameters()
            {
                X = x,
                Y = y
            };
            ViewModel.Movement.RollCommand.Execute(parameters);
        }
    }
}
