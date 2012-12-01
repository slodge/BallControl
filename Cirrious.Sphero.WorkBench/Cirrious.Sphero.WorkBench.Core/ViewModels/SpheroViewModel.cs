// <copyright file="SpheroViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.MvvmCross.Plugins.XamPhotos;
using Cirrious.Sphero.WorkBench.Core.Interfaces;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels
{
    public class SpheroViewModel : BaseViewModel, ISpheroParentViewModel
    {
        private readonly string _name;
        private IConnectedSphero _connectedSphero;
        private bool _shutdownCalled;
        private bool _isConnecting;

        public SpheroMovementViewModel Movement { get; private set; }

        public SpheroAccelMovementViewModel AccelMovement { get; private set; }

        public SpheroColorViewModel Color { get; private set; }

        public SpheroHeadingViewModel Heading { get; private set; }

        public SpheroSetupViewModel Setup { get; private set; }

        public SpheroSpeechViewModel Speech { get; private set; }

        public bool IsConnecting
        {
            get { return _isConnecting; }
            set
            {
                _isConnecting = value;
                RaisePropertyChanged(() => IsConnecting);
            }
        }

        private readonly List<ISpheroChildViewModel> _childViewModels;

        public SpheroViewModel(string name)
        {
            _name = name;
            Movement = new SpheroMovementViewModel(this);
            AccelMovement = new SpheroAccelMovementViewModel(this);
            Color = new SpheroColorViewModel(this);
            Heading = new SpheroHeadingViewModel(this);
            Setup = new SpheroSetupViewModel(this);
            Speech = new SpheroSpeechViewModel(this);

            _childViewModels = new List<ISpheroChildViewModel>
                {
                    Movement,
                    Color,
                    Heading,
                    AccelMovement,
                    Setup,
                    Speech
                };

            DoConnect();
        }

        protected override void Shutdown()
        {
            _shutdownCalled = false;
            ClearConnectedSphero();
            base.Shutdown();
        }

        public ICommand TakePhotoCommand
        {
            get { return new MvxRelayCommand(DoTakePhoto); }
        }

        private void DoTakePhoto()
        {
            Cirrious.MvvmCross.Plugins.XamPhotos.PluginLoader.Instance.EnsureLoaded();
            var photoPicker = this.GetService<IPhotoPicker>();
            photoPicker.TakeAndStorePhoto(result =>
                {
                    /* ignored for now */
                });
        }

        private void ClearConnectedSphero()
        {
            lock (this)
            {
                if (ConnectedSphero != null)
                {
                    ConnectedSphero.Disconnected -= ConnectedSpheroOnDisconnected;
                    ConnectedSphero.Disconnect();
                    ConnectedSphero = null;
                }
            }
        }

        private void DoConnect()
        {
            if (_shutdownCalled)
            {
                return;
            }

            lock (this)
            {
                if (IsConnecting)
                {
                    return;
                }

                // note that we update IsConnecting a bit funny here - to avoid problems with threadlock
                _isConnecting = true;
            }

            RaisePropertyChanged(() => IsConnecting);

            var listService = this.GetService<ISpheroListService>();
            var availableSphero = listService.AvailableSpheros.FirstOrDefault(s => s.Name == _name);

            if (availableSphero == null)
            {
                // TODO - signal the error
                return;
            }

            availableSphero.Connect(OnConnectionSuccess, OnConnectionError);
        }

        private void OnConnectionSuccess(IConnectedSphero sphero)
        {
            lock (this)
            {
                if (_shutdownCalled)
                {
                    sphero.Disconnect();
                    return;
                }

                IsConnecting = false;
                ConnectedSphero = sphero;
                ConnectedSphero.Disconnected += ConnectedSpheroOnDisconnected;
                foreach (var c in _childViewModels)
                {
                    c.OnSpheroConnected();
                }
            }
        }

        private void ConnectedSpheroOnDisconnected(object sender, EventArgs eventArgs)
        {
            ClearConnectedSphero();
        }

        private void OnConnectionError(Exception obj)
        {
            // TODO - signal the error  

            // clear connecting and try again...
            IsConnecting = false;
        }

        public IConnectedSphero ConnectedSphero
        {
            get { return _connectedSphero; }
            private set
            {
                _connectedSphero = value;
                RaisePropertyChanged(() => ConnectedSphero);
            }
        }

        public ICommand CheckConnectionCommand
        {
            get { return new MvxRelayCommand(DoCheckConnection); }
        }

        private void DoCheckConnection()
        {
            if (!_shutdownCalled
                && !_isConnecting
                && ConnectedSphero == null)
            {
                DoConnect();
            }
        }
    }
}