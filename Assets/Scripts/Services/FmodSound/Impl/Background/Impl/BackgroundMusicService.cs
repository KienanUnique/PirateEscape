using Db.Sounds;
using FMODUnity;
using Services.FmodSound.Utils;
using UnityEngine;
using Zenject;

namespace Services.FmodSound.Impl.Background.Impl
{
    public class BackgroundMusicService : IInitializable, IBackgroundMusicService
    {
        private const string EmitterGameObjectName = "StudioEventEmitter";
        
        private readonly ISoundFxBase _soundFxBase;
        private StudioEventEmitter _emitter;

        public BackgroundMusicService(ISoundFxBase soundFxBase)
        {
            _soundFxBase = soundFxBase;
        }

        public void Initialize()
        {
            _emitter = CreateEmitter();
        }

        public void Play()
        {
            if (_emitter.IsPlaying())
                return;
            
            _emitter.Play();
        }

        public void SetPause(bool isPaused)
        {
            _emitter.EventInstance.setPaused(isPaused);
        }

        public void Stop()
        {
            _emitter.Stop();
        }

        private StudioEventEmitter CreateEmitter()
        {
            var eventReference = _soundFxBase.BackgroundMusicEventReference;

            var go = new GameObject($"{EmitterGameObjectName} Background");
            var source = go.AddComponent<StudioEventEmitter>();
            source.EventReference = eventReference;
            
            Object.DontDestroyOnLoad(go);

            return source;
        }
    }
}