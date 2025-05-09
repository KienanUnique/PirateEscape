using System;
using Db.Sounds;
using FMOD.Studio;
using R3;
using Services.FmodSound.Utils;
using Services.Settings;
using Zenject;

namespace Services.FmodSound.Impl
{
    public class GlobalSoundsService : IGlobalSoundsService, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private readonly ISoundFxBase _soundFxBase;
        private readonly ISettingsStorageService _settingsStorageService;

        public GlobalSoundsService(
            ISoundFxBase soundFxBase, 
            ISettingsStorageService settingsStorageService
        )
        {
            _soundFxBase = soundFxBase;
            _settingsStorageService = settingsStorageService;
        }

        public void Initialize()
        {
            _settingsStorageService.MusicVolume.Subscribe(OnMusicVolume).AddTo(_compositeDisposable);
            _settingsStorageService.MasterVolume.Subscribe(OnMasterVolume).AddTo(_compositeDisposable);
            _settingsStorageService.IsMusicEnabled.Subscribe(OnIsMusicEnabled).AddTo(_compositeDisposable);
            _settingsStorageService.IsMasterSoundsEnabled.Subscribe(OnIsSoundsEnabled).AddTo(_compositeDisposable);
        }
        
        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void OnMusicVolume(float volume)
        {
            var bus = GetBus(ESoundsGroupType.Music);
            bus.setVolume(volume);
        }
        
        private void OnMasterVolume(float volume)
        {
            var bus = GetBus(ESoundsGroupType.All);
            bus.setVolume(volume);
        }

        private void OnIsMusicEnabled(bool isEnabled)
        {
            var bus = GetBus(ESoundsGroupType.Music);
            bus.setMute(!isEnabled);
        }
        
        private void OnIsSoundsEnabled(bool isEnabled)
        {
            var bus = GetBus(ESoundsGroupType.All);
            bus.setMute(!isEnabled);
        }

        public void SetPause(ESoundsGroupType groupType, bool isPaused)
        {
            var bus = GetBus(groupType);
            bus.setPaused(isPaused);
        }

        public void SetMute(ESoundsGroupType groupType, bool isMute)
        {
            var bus = GetBus(groupType);
            bus.setMute(isMute);
        }

        public void Stop(ESoundsGroupType groupType, STOP_MODE stopMode)
        {
            var bus = GetBus(groupType);
            bus.stopAllEvents(stopMode);
        }
        
        private Bus GetBus(ESoundsGroupType soundsGroupType)
        {
            var busName = _soundFxBase.SoundGroupBuses[soundsGroupType];
            var bus = FMODUnity.RuntimeManager.GetBus(busName);
            return bus;
        }
    }
}