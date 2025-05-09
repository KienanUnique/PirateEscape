using Game.Db.Dialog;
using Game.Services.Dialog;
using KoboldUi.Element.Controller;
using R3;

namespace Game.Ui.Dialog.Dialog
{
    public class DialogController : AUiController<DialogView>
    {
        private readonly IAvatarBase _avatarStorage;
        private readonly IDialogService _dialogService;
        private readonly IDialogParameters _dialogParameters;

        private bool _isAvatarSet;

        public DialogController(
            IAvatarBase avatarStorage, 
            IDialogService dialogService,
            IDialogParameters dialogParameters
        )
        {
            _avatarStorage = avatarStorage;
            _dialogService = dialogService;
            _dialogParameters = dialogParameters;
        }

        public override void Initialize()
        {
            _dialogService.NeedStartDialog.Subscribe(OnNeedStartDialog).AddTo(View);
            View.Runner.onDialogueComplete.AsObservable().Subscribe(_ => OnDialogComplete()).AddTo(View);
            View.Runner.AddCommandHandler<string>(_dialogParameters.ChangeAvatarCommandName, TryChangeAvatar);

            View.HideAvatarInstantly();
        }

        protected override void OnClose()
        {
            _isAvatarSet = false;
        }

        private void OnNeedStartDialog(IDialogProvider dialogProvider)
        {
            View.Runner.StartDialogue(dialogProvider.StartNode);
        }
        
        private void TryChangeAvatar(string newAvatarName)
        {
            var newAvatarSprite = _avatarStorage.GetAvatarByName(newAvatarName);
            View.ChangeAvatar(newAvatarSprite, _isAvatarSet);
        }
        
        private void OnDialogComplete()
        {
            _dialogService.HandleDialogComplete();
        }
    }
}