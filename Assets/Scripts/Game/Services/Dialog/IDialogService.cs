using Game.Db.Dialog;
using R3;

namespace Game.Services.Dialog
{
    public interface IDialogService
    {
        Observable<Unit> DialogStarted { get; }
        Observable<Unit> DialogComplete { get; }
        Observable<IDialogProvider> NeedStartDialog { get; }
        Observable<Unit> WinRequested { get; }
        Observable<Unit> LoseRequested { get; }
        
        void StartDialog(IDialogProvider dialog);
        void HandleDialogComplete();
        void RequestWin();
        void RequestLose();
    }
}