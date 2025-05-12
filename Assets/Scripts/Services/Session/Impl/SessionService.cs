using Utils;

namespace Services.Session.Impl
{
    public class SessionService : ISessionService
    {
        public EWinEnding WinEnding { get; private set; }
        public bool IsFirstGameStart { get; private set; } = true;

        public void HandleLevelStart()
        {
            IsFirstGameStart = false;
            WinEnding = EWinEnding.None;
        }

        public void HandleWin(EWinEnding winEnding)
        {
            WinEnding = winEnding;
        }
    }
}