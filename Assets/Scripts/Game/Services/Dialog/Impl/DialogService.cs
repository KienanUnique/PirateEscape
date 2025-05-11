using Game.Db.Dialog;
using R3;
using UnityEngine;
using Utils;

namespace Game.Services.Dialog.Impl
{
    public class DialogService : IDialogService
    {
        private readonly ReactiveCommand _dialogStarted = new();
        private readonly ReactiveCommand _dialogComplete = new();
        private readonly ReactiveCommand<EWinEnding> _winRequested = new();
        private readonly ReactiveCommand _loseRequested = new();
        private readonly ReactiveCommand<IDialogProvider> _needStartDialog = new();
        
        private readonly IDialogParameters _dialogParameters;

        private IDialogProvider _currentDialog = null;

        public Observable<Unit> DialogStarted => _dialogStarted;
        public Observable<Unit> DialogComplete => _dialogComplete;
        public Observable<IDialogProvider> NeedStartDialog => _needStartDialog;
        public Observable<EWinEnding> WinRequested => _winRequested;
        public Observable<Unit> LoseRequested => _loseRequested;

        public DialogService(IDialogParameters dialogParameters)
        {
            _dialogParameters = dialogParameters;
        }

        public void StartDialog(IDialogProvider dialog)
        {
            if (_currentDialog != null)
            {
                Debug.LogError($"{nameof(DialogService)} | Already in dialog: {_currentDialog.StartNode}");
                return;
            }

            _currentDialog = dialog;
            _dialogStarted.Execute(Unit.Default);
            _needStartDialog.Execute(dialog);
        }

        public void HandleDialogComplete()
        {
            if (_currentDialog == null)
            {
                Debug.LogError($"{nameof(DialogService)} | Can not finish not started dialog!");
                return;
            }
            
            _currentDialog = null;
            _dialogComplete.Execute(Unit.Default);
        }

        public void RequestWin(string winEndingName)
        {
            var ending = _dialogParameters.GetEndingByName(winEndingName);
            _winRequested.Execute(ending);
        }

        public void RequestLose()
        {
            _loseRequested.Execute(Unit.Default);
        }
    }
}