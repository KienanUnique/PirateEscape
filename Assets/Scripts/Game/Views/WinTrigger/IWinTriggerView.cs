using R3;

namespace Game.Views.WinTrigger
{
    public interface IWinTriggerView
    {
        Observable<Unit> PlayerEntered { get; }
    }
}