// <copyright file="PhotoPicker.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Interfaces.Platform.Diagnostics;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Platform.Diagnostics;
using Xamarin.Media;

namespace Cirrious.MvvmCross.Plugins.XamPhotos.WinRT
{
    public class PhotoPicker : IPhotoPicker, IMvxServiceConsumer
    {
        public PhotoPicker()
        {
            PictureFolder = "XamPhotos";
        }

        public string PictureFolder { get; set; }

        public void TakeAndStorePhoto(Action<string> onPhotoTaken)
        {
            var picker = new MediaPicker();
            if (!picker.IsCameraAvailable || !picker.PhotosSupported)
            {
                MvxTrace.Trace(MvxTraceLevel.Diagnostic, "Photo taking not available");
                return;
            }

            var photoName = string.Format("Sphero{0:yyyyMMddHHmmss}.jpg", DateTime.Now);
            picker.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Name = photoName,
                    Directory = PictureFolder
                })
                  .ContinueWith(t =>
                      {
                          if (t.IsCanceled)
                              return;

#warning Code to do...
                          //var file = this.GetService<IMvxSimpleFileStoreService>();
                          //file.WriteFile(t.Result.Path, stream => t.Result.GetStream().CopyTo(stream));
                          //file.WriteFile("Test.txt", "Some text");
                          onPhotoTaken(t.Result.Path);
                      });
        }

        public void ListPhotoPath(Action<IList<string>> onPhotosListed)
        {
#warning Code to do...
            /*
            ThreadPool.QueueUserWorkItem(ignored =>
                {
                    List<string> files;
                    using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        var path = (PictureFolder) + "/*";
                        files = isf.GetFileNames(path).Select(x => (PictureFolder) + "/" + x).ToList();
                    }

                    onPhotosListed(files.ToList());
                });
             */
        }

        public void Share(string path)
        {
#warning Code to do...
            /*
            var file = this.GetService<IMvxSimpleFileStoreService>();
            using (var ms = new MemoryStream())
            {
                file.TryReadBinaryFile(path, stream =>
                    {
                        stream.CopyTo(ms);
                        return true;
                    });
                ms.Seek(0, SeekOrigin.Begin);
                var lib = new MediaLibrary();
                var picture = lib.SavePicture(string.Format("test.jpg"), ms);
                var task = new ShareMediaTask();
                task.FilePath = picture.GetPath();
                task.Show();
            }
             */
        }
    }
}