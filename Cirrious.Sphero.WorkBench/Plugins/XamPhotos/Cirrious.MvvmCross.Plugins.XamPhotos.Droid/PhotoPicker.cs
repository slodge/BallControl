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
using Cirrious.MvvmCross.Droid.Interfaces;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Platform.Diagnostics;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Platform.Diagnostics;
using Xamarin.Media;

namespace Cirrious.MvvmCross.Plugins.XamPhotos.Droid
{
    public class PhotoPicker
        : IPhotoPicker
          , IMvxServiceConsumer
    {
        public PhotoPicker()
        {
            PictureFolder = "XamPhotos";
        }

        public string PictureFolder { get; set; }

        public void TakeAndStorePhoto(Action<string> onPhotoTaken)
        {
            var currentActivityService = this.GetService<IMvxAndroidCurrentTopActivity>();
            var currentActivity = currentActivityService.Activity;

            var picker = new MediaPicker(currentActivity);
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

                          onPhotoTaken(t.Result.Path);
                      });
        }

        public void ListPhotoPath(Action<IList<string>> onPhotosListed)
        {
            // TODO - list in PictureFolder
            throw new NotImplementedException("TODO");
            //this.GetService<>()
        }

        public void Share(string path)
        {
            throw new NotImplementedException();
        }
    }
}