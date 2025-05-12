using System;
using FMODUnity;
using R3;
using Services.FmodSound.Impl.Game;
using Services.FmodSound.Utils;
using Services.Input;
using Zenject;

namespace Game.Services.ParrotShout
{
    public class ParrotShoutService : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        
        private readonly IInputService _inputService;
        private readonly IGameSoundFxService _gameSoundFxService;
        private StudioEventEmitter _emitter;

        public ParrotShoutService(
            IInputService inputService, 
            IGameSoundFxService gameSoundFxService
        )
        {
            _inputService = inputService;
            _gameSoundFxService = gameSoundFxService;
        }

        public void Initialize()
        {
            _emitter = _gameSoundFxService.GetEmitter(EGameSoundFxType.Shout);
            _inputService.ShoutPerformed.Subscribe(_ => OnShoutPerformed()).AddTo(_disposables);
        }

        public void Dispose()
        {
            _emitter = null;
            _disposables?.Dispose();
        }

        private void OnShoutPerformed()
        {
            if (_emitter == null || _emitter.IsPlaying())
                return;
            
            _emitter.Play();
        }
    }
}