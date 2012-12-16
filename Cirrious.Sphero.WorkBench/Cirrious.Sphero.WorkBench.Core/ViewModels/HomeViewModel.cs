// <copyright file="HomeViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.Sphero.WorkBench.Core.Interfaces;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly ISpheroListService _listService;

        public HomeViewModel()
        {
            _listService = this.GetService<ISpheroListService>();
        }

        public ISpheroListService ListService
        {
            get { return _listService; }
        }

        public ICommand RefreshListCommand
        {
            get { return new MvxRelayCommand(DoRefreshList); }
        }

        public ICommand GoToSpheroCommand
        {
            get { return new MvxRelayCommand<IAvailableSphero>(DoGoToSphero); }
        }

        public ICommand GoToAboutCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<AboutViewModel>()); }
        }

        public ICommand GoToGangnamStyleCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<GangnamStyleViewModel>()); }
        }

        private void DoRefreshList()
        {
            _listService.RefreshList();
        }

        private void DoGoToSphero(IAvailableSphero sphero)
        {
            this.RequestNavigate<SpheroViewModel>(new {name = sphero.Name});
        }
    }
}