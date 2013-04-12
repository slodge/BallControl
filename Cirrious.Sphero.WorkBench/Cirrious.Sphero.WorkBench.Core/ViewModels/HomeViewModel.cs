// <copyright file="HomeViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.Sphero.WorkBench.Core.Interfaces;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly ISpheroListService _listService;

        public HomeViewModel()
        {
            _listService = Mvx.Resolve<ISpheroListService>();
        }

        public ISpheroListService ListService
        {
            get { return _listService; }
        }

        public ICommand RefreshListCommand
        {
            get { return new MvxCommand(DoRefreshList); }
        }

        public ICommand GoToSpheroCommand
        {
            get { return new MvxCommand<IAvailableSphero>(DoGoToSphero); }
        }

        public ICommand GoToAboutCommand
        {
            get { return new MvxCommand(() => ShowViewModel<AboutViewModel>()); }
        }

        public ICommand GoToGangnamStyleCommand
        {
            get { return new MvxCommand(() => ShowViewModel<GangnamStyleViewModel>()); }
        }

        private void DoRefreshList()
        {
            _listService.RefreshList();
        }

        private void DoGoToSphero(IAvailableSphero sphero)
        {
            this.ShowViewModel<SpheroViewModel>(new {name = sphero.Name});
        }
    }
}