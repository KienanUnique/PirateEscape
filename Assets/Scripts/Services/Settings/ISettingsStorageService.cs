using R3;

namespace Services.Settings
{
    public interface ISettingsStorageService
    {
        ReadOnlyReactiveProperty<float> MasterVolume { get; }
        ReadOnlyReactiveProperty<float> MusicVolume { get; }
        ReadOnlyReactiveProperty<float> MusicBaseVolume { get; }
        ReadOnlyReactiveProperty<float> VideosVolume { get; }
        ReadOnlyReactiveProperty<float> VideosBaseVolume { get; }
        ReadOnlyReactiveProperty<bool> IsMasterSoundsEnabled { get; }
        ReadOnlyReactiveProperty<bool> IsMusicEnabled { get; }
        ReadOnlyReactiveProperty<bool> IsDefaultSettingsApplied { get; }

        void SetMasterVolume(float newSoundVolume);
        void SetMusicVolume(float newSoundVolume);
        void SetIsSoundsEnabled(bool isSoundsEnabled);
        void SetIsMusicEnabled(bool isMusicEnabled);
    }
}