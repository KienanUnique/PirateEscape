namespace Services.FmodSound.Impl.Background
{
    public interface IBackgroundMusicService
    {
        void Play();
        void SetPause(bool isPaused);
        void Stop();
    }
}