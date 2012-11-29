// <copyright file="SpheroColorViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Plugins.Color;
using Cirrious.MvvmCross.Plugins.Sphero.Commands;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class SpheroColorViewModel : BaseSpheroChildViewModel
    {
        private MvxColor _color;

        public SpheroColorViewModel(ISpheroParentViewModel parent)
            : base(parent)
        {
            _color = new MvxColor(0);
        }

        public int Red
        {
            get { return _color.R; }
            set
            {
                _color.R = value;
                RaisePropertyChanged(() => Red);
                OnUserColorComponentChange();
            }
        }

        public int Green
        {
            get { return _color.G; }
            set
            {
                _color.G = value;
                RaisePropertyChanged(() => Green);
                OnUserColorComponentChange();
            }
        }

        public int Blue
        {
            get { return _color.B; }
            set
            {
                _color.B = value;
                RaisePropertyChanged(() => Blue);
                OnUserColorComponentChange();
            }
        }

        public MvxColor Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChanged(() => Color);
            }
        }

        private void OnUserColorComponentChange()
        {
            RaisePropertyChanged(() => Color);
            var command = new SetColorLedCommand(new MvxColor(_color.R, _color.G, _color.B));
            SendCommand(command);
        }

        public override void OnSpheroConnected()
        {
            base.OnSpheroConnected();
            SendCommand(new GetColorLedCommand());
        }

        protected override void OnCommandSuccess(ISpheroMessage message)
        {
            if (message is GetColorLedCommand.ColorMessage)
            {
                var colorMessage = message as GetColorLedCommand.ColorMessage;
                Color = colorMessage.Color;
                RaisePropertyChanged(() => Red);
                RaisePropertyChanged(() => Green);
                RaisePropertyChanged(() => Blue);
            }
            base.OnCommandSuccess(message);
        }
    }
}