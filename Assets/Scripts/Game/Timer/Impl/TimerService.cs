using System;
using Game.Db.Timer;
using R3;

namespace Game.Timer.Impl
{
    public class TimerService : ITimerService, IDisposable
    {
        private readonly TimeSpan _oneSecondTimeSpan = TimeSpan.FromSeconds(1f);
        private readonly ReactiveCommand _timeElapsed = new();
        private readonly ReactiveProperty<TimeSpan> _remainingTime = new(TimeSpan.Zero);
        
        private readonly ITimerParameters _timerParameters;
        
        private IDisposable _timerDisposable;

        public Observable<Unit> TimerEnded => _timeElapsed;
        public ReadOnlyReactiveProperty<TimeSpan> RemainingTime => _remainingTime;

        public TimerService(ITimerParameters timerParameters)
        {
            _timerParameters = timerParameters;
        }

        public void StartLoseTimer()
        {
            var timerSeconds = _timerParameters.LoseTimerDurationSeconds;
            
            _remainingTime.Value = TimeSpan.FromSeconds(timerSeconds);
            _timerDisposable = Observable.Interval(_oneSecondTimeSpan)
                .Subscribe(_ => CountDownSecond());
        }

        private void CountDownSecond()
        {
            var newRemainingTime = _remainingTime.Value - _oneSecondTimeSpan;
            if (newRemainingTime.TotalSeconds <= 0)
            {
                _remainingTime.Value = TimeSpan.Zero;
                _timeElapsed.Execute(Unit.Default);
                _timerDisposable?.Dispose();
                
                return;
            }
            
            _remainingTime.Value = newRemainingTime;
        }

        public void StopLoseTimer()
        {
            _timerDisposable?.Dispose();
        }

        public void Dispose()
        {
            _timerDisposable?.Dispose();
        }
    }
}