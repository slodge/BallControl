// <copyright file="SpeechListener.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Platform;
using Windows.Phone.Speech.Recognition;

namespace Cirrious.MvvmCross.Plugins.Speech.WindowsPhone
{
    public class SpeechListener : ISpeechListener
    {
        private bool _pleaseFinish;
        private IList<string> _words;

        public void Start(IList<string> words)
        {
            _pleaseFinish = false;
            _words = words;
            ThreadPool.QueueUserWorkItem(ignored => StartRecognizeAsync());
        }

        private async Task StartRecognizeAsync()
        {
            var speechRecognizer = new SpeechRecognizer();
            speechRecognizer.Grammars.AddGrammarFromList(
                "answer",
                _words);

            while (!_pleaseFinish)
            {
                var result = await speechRecognizer.RecognizeAsync();

                if (result.TextConfidence != SpeechRecognitionConfidence.Rejected)
                {
                    ProcessResult(result);
                }
                else
                {
                    Debug.WriteLine("No text!");
                }
            }
        }

        private void ProcessResult(SpeechRecognitionResult result)
        {
            var handler = Heard;
            if (handler == null)
                return;

            var possibleWord = new PossibleWord
                {
                    ProbablePercent = ToPercent(result.TextConfidence),
                    Word = result.Text
                };

            handler(this, new MvxValueEventArgs<PossibleWord>(possibleWord));
        }

        private double ToPercent(SpeechRecognitionConfidence textConfidence)
        {
            switch (textConfidence)
            {
                case SpeechRecognitionConfidence.Rejected:
                    return 0;
                case SpeechRecognitionConfidence.Low:
                    return 30;
                case SpeechRecognitionConfidence.Medium:
                    return 60;
                case SpeechRecognitionConfidence.High:
                    return 90;
                default:
                    throw new ArgumentOutOfRangeException("textConfidence");
            }
        }

        public void Stop()
        {
            _pleaseFinish = true;
        }

        public event EventHandler<MvxValueEventArgs<PossibleWord>> Heard;
    }
}