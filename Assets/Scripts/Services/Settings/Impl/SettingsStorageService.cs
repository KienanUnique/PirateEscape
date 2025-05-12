using Db.Sounds;
using R3;
using Zenject;

namespace Services.Settings.Impl
{
    public class SettingsStorageService : ISettingsStorageService, IInitializable
    {
        private readonly IGameDefaultParameters _gameDefaultParameters;
        
        private readonly ReactiveProperty<float> _masterSoundsVolume = new(1f);
        
        private readonly ReactiveProperty<float> _musicVolume = new(1f);
        private readonly ReactiveProperty<float> _musicBaseVolume = new(1f);
        
        private readonly ReactiveProperty<float> _videosVolume = new(1f);
        private readonly ReactiveProperty<float> _videosBaseVolume = new(1f);
        
        private readonly ReactiveProperty<bool> _isSoundsEnabled = new(true);
        private readonly ReactiveProperty<bool> _isMusicEnabled = new(true);
        private readonly ReactiveProperty<bool> _isDefaultSettingsApplied = new(false);

        public ReadOnlyReactiveProperty<float> MasterVolume => _masterSoundsVolume;
        
        public ReadOnlyReactiveProperty<float> MusicVolume => _musicVolume;
        public ReadOnlyReactiveProperty<float> MusicBaseVolume => _musicBaseVolume;
        
        public ReadOnlyReactiveProperty<float> VideosVolume => _videosVolume;
        public ReadOnlyReactiveProperty<float> VideosBaseVolume => _videosBaseVolume;
        
        public ReadOnlyReactiveProperty<bool> IsMasterSoundsEnabled => _isSoundsEnabled;
        public ReadOnlyReactiveProperty<bool> IsMusicEnabled => _isMusicEnabled;
        
        public ReadOnlyReactiveProperty<bool> IsDefaultSettingsApplied => _isDefaultSettingsApplied;
        public float MouseSensitivity { get; private set; }

        public SettingsStorageService(IGameDefaultParameters gameDefaultParameters)
        {
            _gameDefaultParameters = gameDefaultParameters;
        }
        
        public void Initialize()
        {
            _masterSoundsVolume.Value = _gameDefaultParameters.MasterVolume;
            
            _musicBaseVolume.Value = _gameDefaultParameters.MusicVolume;
            _videosBaseVolume.Value = _gameDefaultParameters.FinalTitlesVolume;
            
            MouseSensitivity = _gameDefaultParameters.MouseSensitivity;

            UpdateFinalSounds();
            _isDefaultSettingsApplied.Value = true;
        }

        public void SetMasterVolume(float newSoundVolume)
        {
            _masterSoundsVolume.Value = newSoundVolume;
            UpdateFinalSounds();
        }

        public void SetMusicVolume(float newSoundVolume)
        {
            _musicBaseVolume.Value = newSoundVolume;
            UpdateFinalSounds();
        }

        public void SetIsSoundsEnabled(bool isSoundsEnabled) => _isSoundsEnabled.Value = isSoundsEnabled;
        public void SetIsMusicEnabled(bool isMusicEnabled) => _isMusicEnabled.Value = isMusicEnabled;
        public void SetMouseSensitivity(float newSensitivity)
        {
            MouseSensitivity = newSensitivity;
        }

        private void UpdateFinalSounds()
        {
            _musicVolume.Value = _musicBaseVolume.Value * _masterSoundsVolume.Value;
            _videosVolume.Value = _videosBaseVolume.Value * _masterSoundsVolume.Value;
        }
    }
}