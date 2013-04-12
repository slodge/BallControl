// <copyright file="PathToImageConverter.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.File;

namespace Cirrious.Sphero.WorkBench.UI.WindowsPhone.NativeConverters
{
    public class PathToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] contents = null;
            try
            {
                var file = Mvx.Resolve<IMvxFileStore>();
                file.TryReadBinaryFile((string) value, out contents);
            }
            catch (Exception)
            {
                // masked
            }

            if (contents == null)
                return null;

            using (var stream = new MemoryStream(contents))
            {
                var image = new BitmapImage();
                image.SetSource(stream);
                return image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}