using Game.Db.Dialog;
using R3;
using UnityEngine;

namespace Game.Services.Dialog.Impl
{
    public class DialogService : IDialogService
    {
        private readonly ReactiveCommand _dialogStarted = new();
        private readonly ReactiveCommand _dialogComplete = new();
        private readonly ReactiveCommand<IDialogProvider> _needStartDialog = new();

        private IDialogProvider _currentDialog = null;

        public Observable<Unit> DialogStarted => _dialogStarted;
        public Observable<Unit> DialogComplete => _dialogComplete;
        public Observable<IDialogProvider> NeedStartDialog => _needStartDialog;
        
        public void StartDialog(IDialogProvider dialog)
        {
            if (_currentDialog != null)
            {
                Debug.LogError($"{nameof(DialogService)} | Already in dialog: {_currentDialog.StartNode}");
                return;
            }

            _currentDialog = dialog;
            _dialogStarted.Execute(Unit.Default);
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
    }
}