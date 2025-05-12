using System;
using Game.Timer;
using KoboldUi.Element.Controller;
using R3;
using UnityEngine;

namespace Game.Ui.Gameplay.Timer
{
    public class TimerController : AUiController<TimerView>
    {
        private readonly ITimerService _timerService;
        
        private Color _initialColor;
        private bool _isTimerBlocked;

        public TimerController(ITimerService timerService)
        {
            _timerService = timerService;
        }

        public override void Initialize()
        {
            _timerService.RemainingTime.Subscribe(UpdateTimer).AddTo(View);
            _timerService.TimerEnded.Subscribe(_ => OnTimerEnd()).AddTo(View);

            _initialColor = View.TimerText.color;
            
            UpdateTimer(_timerService.RemainingTime.CurrentValue);
        }

        private void UpdateTimer(TimeSpan timeSpan)
        {
            if(_isTimerBlocked)
                return;

            var text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            View.TimerText.text = text;
        }

        private void OnTimerEnd()
        {
            if(View.CountOfBlinks == 0)
                return;

            _isTimerBlocked = true;
            
            var countOfTicks = View.CountOfBlinks * 2 - 1;
            
            Observable.Timer(TimeSpan.FromSeconds(View.BlinkVisibleDelay * countOfTicks))
                .Subscribe(_ =>
                {
                    _isTimerBlocked = false;

                    if (_timerService.RemainingTime.CurrentValue != TimeSpan.Zero)
                        UpdateTimer(_timerService.RemainingTime.CurrentValue);
                    else
                        View.TimerText.text = "--:--";
                }).AddTo(View);
            
            View.TimerText.color = View.TimerEndColor;
            
            var changeColor = false;
            Observable.Interval(TimeSpan.FromSeconds(View.BlinkVisibleDelay))
                .Take(countOfTicks)
                .Subscribe(x =>
                {
                    var nextColor = changeColor ? View.TimerEndColor : _initialColor;
                    View.TimerText.color = nextColor;
                    changeColor = !changeColor;
                })
                .AddTo(View);
        }
    }
}