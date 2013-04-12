// <copyright file="SpheroSetupViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Sphero.Commands;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class SpheroSetupViewModel : BaseSpheroChildViewModel
    {
        private bool _bacKLedOn;

        public SpheroSetupViewModel(ISpheroParentViewModel parent) : base(parent)
        {
        }

        public bool BackLedOn
        {
            get { return _bacKLedOn; }
            set
            {
                _bacKLedOn = value;
                var command = new BackLedCommand(value ? 255 : 0);
                SendCommand(command);
                RaisePropertyChanged(() => BackLedOn);
            }
        }
    }
}