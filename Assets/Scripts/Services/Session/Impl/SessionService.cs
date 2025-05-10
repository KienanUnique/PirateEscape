namespace Services.Session.Impl
{
    public class SessionService : ISessionService
    {
        public bool IsFirstGameStart { get; private set; } = true;

        public void HandleLevelStart()
        {
            IsFirstGameStart = false;
        }
    }
}