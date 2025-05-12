using R3;
using Utils;

namespace Game.Views.WinTrigger
{
    public interface IWinTriggerView
    {
        Observable<EWinEnding> WinRequested { get; }
    }
}