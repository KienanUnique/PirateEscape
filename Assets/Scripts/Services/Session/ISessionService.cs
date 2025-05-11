using Utils;

namespace Services.Session
{
    public interface ISessionService
    {
        EWinEnding WinEnding { get; }
        bool IsFirstGameStart { get; }
        void HandleLevelStart();
        void HandleWin(EWinEnding winEnding);
    }
}