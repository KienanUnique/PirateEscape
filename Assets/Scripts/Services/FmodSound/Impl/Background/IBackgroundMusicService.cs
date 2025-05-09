using Services.FmodSound.Utils;

namespace Services.FmodSound.Impl.Background
{
    public interface IBackgroundMusicService
    {
        void Play(EBackgroundMusicType soundType);
        void SetPause(bool isPaused);
        void Stop();
    }
}