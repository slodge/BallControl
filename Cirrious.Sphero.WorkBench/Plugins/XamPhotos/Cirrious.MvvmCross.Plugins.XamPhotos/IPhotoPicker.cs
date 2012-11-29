// <copyright file="IPhotoPicker.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;

namespace Cirrious.MvvmCross.Plugins.XamPhotos
{
    public interface IPhotoPicker
    {
        string PictureFolder { get; set; }
        void TakeAndStorePhoto(Action<string> onPhotoTaken);
        void ListPhotoPath(Action<IList<string>> onPhotosListed);
        void Share(string path);
    }
}