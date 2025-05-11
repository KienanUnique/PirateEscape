using System;
using R3;

namespace Game.Timer
{
    public interface ITimerService
    {
        Observable<Unit> TimerEnded { get; }
        ReadOnlyReactiveProperty<TimeSpan> RemainingTime { get; }
        
        void StartLoseTimer();
        void StopLoseTimer();
        void SetPause(bool isPaused);
    }
}