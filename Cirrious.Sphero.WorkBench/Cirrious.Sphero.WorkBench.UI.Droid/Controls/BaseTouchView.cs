// <copyright file="BaseTouchView.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace Cirrious.Sphero.WorkBench.UI.Droid.Controls
{
    public class BaseTouchView : View
    {
        protected const int MaximumTouchCount = 10;

        private readonly Paint _paint = new Paint(PaintFlags.AntiAlias);
        private readonly float[] _x = new float[MaximumTouchCount];
        private readonly float[] _y = new float[MaximumTouchCount];
        private readonly bool[] _isTouch = new bool[MaximumTouchCount];

        public BaseTouchView(Context context)
            : base(context)
        {
        }

        public BaseTouchView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public BaseTouchView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
        }

        public bool GetTouchState(int touchPoint, out float x, out float y)
        {
            if (touchPoint >= MaxTouchesAllowed)
            {
                x = 0.0f;
                y = 0.0f;
                return false;
            }

            x = _x[touchPoint];
            y = _y[touchPoint];
            return _isTouch[touchPoint];
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            SetMeasuredDimension(MeasureSpec.GetSize(widthMeasureSpec), MeasureSpec.GetSize(heightMeasureSpec));
        }

        protected virtual int MaxTouchesAllowed
        {
            get { return 1; }
        }

        protected virtual Color GetColor(int touchPoint)
        {
            switch (touchPoint)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Wheat;
                case 5:
                    return Color.Pink;
                case 6:
                    return Color.Purple;
                case 7:
                    return Color.DarkRed;
                case 8:
                    return Color.DarkBlue;
                case 9:
                    return Color.DarkGreen;
                default:
                    return Color.Gray;
            }
        }

        protected virtual float GetWidth(int touchPoint)
        {
            return 75f;
        }

        protected override void OnDraw(Canvas canvas)
        {
            for (int i = 0; i < MaxTouchesAllowed; i++)
            {
                if (_isTouch[i])
                {
                    DrawTouchPoint(canvas, i);
                }
            }
        }

        protected virtual void DrawTouchPoint(Canvas canvas, int i)
        {
            _paint.StrokeWidth = 1;
            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = GetColor(i);
            canvas.DrawCircle(_x[i], _y[i], GetWidth(i), _paint);
        }

        public override bool OnTouchEvent(MotionEvent motionEvent)
        {
            var pointerIndex = ((int) (motionEvent.Action & MotionEventActions.PointerIdMask) >>
                                (int) MotionEventActions.PointerIdShift);
            var pointerId = motionEvent.GetPointerId(pointerIndex);
            var action = (motionEvent.Action & MotionEventActions.Mask);
            var pointCnt = motionEvent.PointerCount;

            if (pointCnt <= MaximumTouchCount)
            {
                if (pointerIndex <= MaximumTouchCount - 1)
                {
                    for (var i = 0; i < pointCnt; i++)
                    {
                        var id = motionEvent.GetPointerId(i);
                        _x[id] = (int) motionEvent.GetX(i);
                        _y[id] = (int) motionEvent.GetY(i);
                    }

                    switch (action)
                    {
                        case MotionEventActions.Down:
                        case MotionEventActions.PointerDown:
                        case MotionEventActions.Move:
                            _isTouch[pointerId] = true;
                            break;
                        default:
                            _isTouch[pointerId] = false;
                            break;
                    }
                }
            }
            Invalidate();
            return true;
        }
    }
}