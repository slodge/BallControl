// <copyright file="SpheroSpeechViewModel.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Plugins.Color;
using Cirrious.MvvmCross.Plugins.Speech;
using Cirrious.MvvmCross.Plugins.Sphero.Commands;

namespace Cirrious.Sphero.WorkBench.Core.ViewModels.SpheroSubViewModels
{
    public class SpheroSpeechViewModel : BaseSpheroMovementViewModel
    {
        private readonly ISpeechListener _speechListener;
        private bool _speechAvailable;
        private bool _speechIsOn;
        private Dictionary<string, MethodInfo> _speechMethods;

        public SpheroSpeechViewModel(ISpheroParentViewModel parent)
            : base(parent)
        {
            Cirrious.MvvmCross.Plugins.Speech.PluginLoader.Instance.EnsureLoaded();
            SpeechAvailable = this.TryGetService(out _speechListener);
            if (_speechListener != null)
            {
                BuildActions();
                HeardSoFar = new ObservableCollection<string>();
                _speechListener.Heard += SpeechListenerOnHeard;
            }
        }

        private void BuildActions()
        {
            var query = from m in this.GetType().GetMethods()
                        let attr =
                            m.GetCustomAttributes(typeof (SpeechActionAttribute), false).FirstOrDefault() as
                            SpeechActionAttribute
                        where attr != null
                        select new
                            {
                                Method = m,
                                attr.Name
                            };

            _speechMethods = query.ToDictionary(x => x.Name, x => x.Method);
        }

        private void SpeechListenerOnHeard(object sender, MvxValueEventArgs<PossibleWord> e)
        {
            if (!SpeechIsOn)
                return;

            MethodInfo methodInfo;
            if (!_speechMethods.TryGetValue(e.Value.Word, out methodInfo))
                return;

            methodInfo.Invoke(this, new object[0]);

            this.InvokeOnMainThread(() =>
                {
                    HeardSoFar.Insert(0, e.Value.Word);
                    while (HeardSoFar.Count > 100)
                    {
                        HeardSoFar.RemoveAt(HeardSoFar.Count - 1);
                    }
                });
        }

        public bool SpeechAvailable
        {
            get { return _speechAvailable; }
            private set
            {
                _speechAvailable = value;
                RaisePropertyChanged(() => SpeechAvailable);
            }
        }

        public bool SpeechIsOn
        {
            get { return _speechIsOn; }
            private set
            {
                _speechIsOn = value;
                RaisePropertyChanged(() => SpeechIsOn);
            }
        }

        public ObservableCollection<string> HeardSoFar { get; private set; }

        public ICommand EnsureSpeechIsOn
        {
            get { return new MvxRelayCommand<bool>(DoEnsureSpeechIs); }
        }

        private void DoEnsureSpeechIs(bool on)
        {
            if (_speechIsOn == on)
                return;

            if (!_speechAvailable)
                return;

            if (on)
            {
                _speechListener.Start(_speechMethods.Keys.ToList());
            }
            else
            {
                _speechListener.Stop();
            }
            SpeechIsOn = on;
        }

        #region Speech Methods

        [SpeechAction("Forwards")]
        public void Forwards()
        {
            SendRollCommand(255, 0);
        }

        [SpeechAction("Reverse")]
        public void Reverse()
        {
            SendRollCommand(255, 180);
        }

        [SpeechAction("Left")]
        public void Left()
        {
            SendRollCommand(255, 270);
        }

        [SpeechAction("Right")]
        public void Right()
        {
            SendRollCommand(255, 0);
        }

        [SpeechAction("Turn 90")]
        public void Turn90()
        {
            SendTurnCommand(90);
        }

        [SpeechAction("Turn 180")]
        public void Turn180()
        {
            SendTurnCommand(180);
        }

        [SpeechAction("Turn 270")]
        public void Turn270()
        {
            SendTurnCommand(270);
        }

        [SpeechAction("Stop")]
        public void Stop()
        {
            // not sure why - but don't send isStop:true at present?!
            SendRollCommand();
        }

        [SpeechAction("End")]
        public void End()
        {
            // not sure why - but don't send isStop:true at present?!
            Stop();
        }

        [SpeechAction("Red")]
        public void Red()
        {
            SendColorCommand(red: 255);
        }

        [SpeechAction("Green")]
        public void Green()
        {
            SendColorCommand(green: 255);
        }

        [SpeechAction("Blue")]
        public void Blue()
        {
            SendColorCommand(blue: 255);
        }

        [SpeechAction("White")]
        public void White()
        {
            SendColorCommand(255, 255, 255);
        }

        [SpeechAction("Black")]
        public void Black()
        {
            SendColorCommand();
        }

        [SpeechAction("Tail On")]
        public void TailOn()
        {
            SendTailCommand(true);
        }

        [SpeechAction("Tail Off")]
        public void TailOff()
        {
            SendTailCommand(false);
        }

        #endregion

        private void SendColorCommand(int red = 0, int green = 0, int blue = 0)
        {
            SendCommand(new SetColorLedCommand(new MvxColor(red, green, blue)));
        }

        private void SendTurnCommand(int turn)
        {
            SendCommand(new HeadingCommand(turn));
        }

        private void SendRollCommand(int speed = 0, int direction = 0, bool isStop = false)
        {
            var scaled = SpeedService.SpeedPercent*speed/100.0;
            SendCommand(new RollCommand((int) scaled, direction, isStop));
        }

        private void SendTailCommand(bool on)
        {
            SendCommand(new BackLedCommand(on ? 255 : 0));
        }
    }
}