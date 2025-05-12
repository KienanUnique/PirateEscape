using Game.Db.Dialog;
using R3;
using Utils;

namespace Game.Services.Dialog
{
    public interface IDialogService
    {
        Observable<Unit> DialogStarted { get; }
        Observable<Unit> DialogComplete { get; }
        Observable<IDialogProvider> NeedStartDialog { get; }
        Observable<EWinEnding> WinRequested { get; }
        Observable<Unit> LoseRequested { get; }
        
        void StartDialog(IDialogProvider dialog);
        void HandleDialogComplete();
        void RequestWin(string winEnding);
        void RequestLose();
    }
}