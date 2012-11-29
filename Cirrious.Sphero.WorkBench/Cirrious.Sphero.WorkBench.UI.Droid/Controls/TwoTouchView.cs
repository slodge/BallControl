// <copyright file="TwoTouchView.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Android.Content;
using Android.Util;

namespace Cirrious.Sphero.WorkBench.UI.Droid.Controls
{
    public class TwoTouchView : BaseTouchView
    {
        protected override int MaxTouchesAllowed
        {
            get { return 2; }
        }

        public TwoTouchView(Context context)
            : base(context)
        {
        }

        public TwoTouchView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public TwoTouchView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
        }
    }
}