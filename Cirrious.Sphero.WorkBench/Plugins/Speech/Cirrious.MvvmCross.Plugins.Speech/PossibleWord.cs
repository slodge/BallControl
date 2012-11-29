// <copyright file="PossibleWord.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

namespace Cirrious.MvvmCross.Plugins.Speech
{
    public class PossibleWord
    {
        // 100.0 is definite, 0.0 is unlikely!
        public double ProbablePercent { get; set; }
        public string Word { get; set; }
    }
}