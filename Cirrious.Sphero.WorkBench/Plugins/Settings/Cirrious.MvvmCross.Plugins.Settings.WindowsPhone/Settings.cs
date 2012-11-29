// <copyright file="Settings.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Microsoft.Phone.Tasks;

namespace Cirrious.MvvmCross.Plugins.Settings.WindowsPhone
{
    public class Settings : ISettings
    {
        public bool CanShow(string which)
        {
            switch (which)
            {
                case KnownSettings.Bluetooth:
                case KnownSettings.Wifi:
                    return true;
            }

            return false;
        }

        public void Show(string which)
        {
            var task = new ConnectionSettingsTask();

            switch (which)
            {
                case KnownSettings.Bluetooth:
                    task.ConnectionSettingsType = ConnectionSettingsType.Bluetooth;
                    break;
                case KnownSettings.Wifi:
                    task.ConnectionSettingsType = ConnectionSettingsType.WiFi;
                    break;
                default:
                    throw new NotSupportedException("Not supported " + which);
            }

            task.Show();
        }
    }
}