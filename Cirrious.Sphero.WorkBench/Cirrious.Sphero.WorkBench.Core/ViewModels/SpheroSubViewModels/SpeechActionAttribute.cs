// <copyright file="SpeechActionAttribute.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class SpeechActionAttribute : Attribute
    {
        public string Name { get; set; }

        public SpeechActionAttribute(string name)
        {
            Name = name;
        }
    }
}