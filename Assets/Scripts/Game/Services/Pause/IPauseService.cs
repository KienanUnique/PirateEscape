using R3;

namespace Game.Services.Pause
{
    public interface IPauseService
    {
        ReadOnlyReactiveProperty<bool> IsPaused { get; }
        void Pause();
        void Unpause();
    }
}