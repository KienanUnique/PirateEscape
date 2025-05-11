namespace Services.Session
{
    public interface ISessionService
    {
        bool IsFirstGameStart { get; }
        void HandleLevelStart();
    }
}