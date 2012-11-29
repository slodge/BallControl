// <copyright file="PhotoListViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Plugins.File;
using Cirrious.MvvmCross.Plugins.XamPhotos;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels
{
    public class PhotoListViewModel : BaseViewModel
    {
        private ObservableCollection<PhotoWithCommands> _photos;

        public PhotoListViewModel()
        {
            var photoPicker = this.GetService<IPhotoPicker>();
            photoPicker.ListPhotoPath(OnPhotoListAvailable);
        }

        private void OnPhotoListAvailable(IList<string> list)
        {
            Photos = new ObservableCollection<PhotoWithCommands>(list.Select(x => new PhotoWithCommands(this, x)));
        }

        public class PhotoWithCommands
        {
            private readonly PhotoListViewModel _parent;

            public PhotoWithCommands(PhotoListViewModel parent, string path)
            {
                _parent = parent;
                Path = path;
            }

            public ICommand ShareCommand
            {
                get { return new MvxRelayCommand(() => _parent.DoShare(this)); }
            }

            public ICommand DeleteCommand
            {
                get { return new MvxRelayCommand(() => _parent.DoDelete(this)); }
            }

            public string Path { get; private set; }
        }

        private void DoDelete(PhotoWithCommands photoWithCommands)
        {
            var file = this.GetService<IMvxSimpleFileStoreService>();
            file.DeleteFile(photoWithCommands.Path);
            Photos.Remove(photoWithCommands);
        }

        private void DoShare(PhotoWithCommands photoWithCommands)
        {
            var picker = this.GetService<IPhotoPicker>();
            picker.Share(photoWithCommands.Path);
        }

        public ObservableCollection<PhotoWithCommands> Photos
        {
            get { return _photos; }
            set
            {
                _photos = value;
                RaisePropertyChanged(() => Photos);
            }
        }
    }
}