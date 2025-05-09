using System;
using R3;
using UnityEngine;

namespace Game.Services.Pause.Impl
{
    public class PauseService : IPauseService, IDisposable
    {
        private readonly ReactiveProperty<bool> _isPaused = new(false);

        public ReadOnlyReactiveProperty<bool> IsPaused => _isPaused;
    
        public void Pause()
        {
            _isPaused.Value = true;
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            Time.timeScale = 1;
            _isPaused.Value = false;
        }

        public void Dispose()
        {
            _isPaused?.Dispose();
            Time.timeScale = 1;
        }
    }
}