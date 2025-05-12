using System;
using Game.Core;
using Game.Timer;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Views.Timer
{
    public class TimerView : AView
    {
        [SerializeField] private TMP_Text _timerText;
        
        [Header("Timer End Animation")] 
        [SerializeField] private int _countOfBlinks = 2;
        [SerializeField] private float _blinkVisibleDelay = 0.5f;
        [SerializeField] private Color _timerEndColor = Color.red;
        
        private Color _initialColor;
        private bool _isTimerBlocked;
        
        [Inject] private ITimerService _timerService;

        protected override void OnInitialize()
        {
            _timerService.RemainingTime.Subscribe(UpdateTimer).AddTo(this);
            _timerService.TimerEnded.Subscribe(_ => OnTimerEnd()).AddTo(this);

            _initialColor = _timerText.color;
            
            UpdateTimer(_timerService.RemainingTime.CurrentValue);
        }
        
        private void UpdateTimer(TimeSpan timeSpan)
        {
            if(_isTimerBlocked)
                return;

            var text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            _timerText.text = text;
        }

        private void OnTimerEnd()
        {
            if(_countOfBlinks == 0)
                return;

            _isTimerBlocked = true;
            
            var countOfTicks = _countOfBlinks * 2 - 1;
            
            Observable.Timer(TimeSpan.FromSeconds(_blinkVisibleDelay * countOfTicks))
                .Subscribe(_ =>
                {
                    _isTimerBlocked = false;

                    if (_timerService.RemainingTime.CurrentValue != TimeSpan.Zero)
                        UpdateTimer(_timerService.RemainingTime.CurrentValue);
                    else
                        _timerText.text = "--:--";
                }).AddTo(this);
            
            _timerText.color = _timerEndColor;
            
            var changeColor = false;
            Observable.Interval(TimeSpan.FromSeconds(_blinkVisibleDelay))
                .Take(countOfTicks)
                .Subscribe(x =>
                {
                    var nextColor = changeColor ? _timerEndColor : _initialColor;
                    _timerText.color = nextColor;
                    changeColor = !changeColor;
                })
                .AddTo(this);
        }
    }
}