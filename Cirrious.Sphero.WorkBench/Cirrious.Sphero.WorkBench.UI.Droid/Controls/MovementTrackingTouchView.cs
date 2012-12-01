// <copyright file="MovementTrackingTouchView.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels;

namespace Cirrious.Sphero.WorkBench.UI.Droid.Controls
{
    public class MovementTrackingTouchView : BaseTouchView
    {
        private bool _isCurrentlyTouched;

        protected override int MaxTouchesAllowed
        {
            get { return 1; }
        }

        private static readonly Color TouchPointColor = new Color(255, 255, 0, 192);

        protected override Color GetColor(int touchPoint)
        {
            return TouchPointColor;
        }

        protected override float GetWidth(int touchPoint)
        {
            return 125.0f;
        }

        public MovementTrackingTouchView(Context context)
            : base(context)
        {
        }

        public MovementTrackingTouchView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public MovementTrackingTouchView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
        }

        public override bool OnTouchEvent(Android.Views.MotionEvent motionEvent)
        {
            var toReturn = base.OnTouchEvent(motionEvent);
            NotifyTouchState();
            return toReturn;
        }

        private void NotifyTouchState()
        {
            float x, y;
            var newTouchState = GetTouchState(0, out x, out y);
            if (newTouchState)
            {
                if (!_isCurrentlyTouched)
                {
                    Fire(TouchStart);
                    _isCurrentlyTouched = true;
                }
                UpdateTouchPosition(x, y);
            }
            else
            {
                if (_isCurrentlyTouched)
                {
                    Fire(TouchEnd);
                    _isCurrentlyTouched = false;
                }
            }
        }

        private void UpdateTouchPosition(float x, float y)
        {
            var height = this.Height;
            var width = this.Width;
            var minimal = Math.Min(height, width);
            var relativeX = 2*(x - width/2)/minimal;
            var relativeY = 2*(y - height/2)/minimal;

            if (relativeX > 1.0f)
                relativeX = 1.0f;
            if (relativeX < -1.0f)
                relativeX = -1.0f;

            if (relativeY > 1.0f)
                relativeY = 1.0f;
            if (relativeY < -1.0f)
                relativeY = -1.0f;

            // note that we rotate x and y here because we want 0 to be up the screen
            TouchPosition = new CartesianPositionParameters {Y = relativeX, X = -relativeY};
            Fire(TouchPositionChanged);
        }

        private void Fire(EventHandler handler)
        {
            if (handler == null)
                return;

            handler(this, EventArgs.Empty);
        }

        public CartesianPositionParameters TouchPosition { get; set; }

        public event EventHandler TouchStart;

        public event EventHandler TouchEnd;

        public event EventHandler TouchPositionChanged;
    }
}