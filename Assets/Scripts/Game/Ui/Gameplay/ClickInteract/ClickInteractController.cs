using Game.Views;
using Game.Views.Player;
using KoboldUi.Element.Controller;
using R3;

namespace Game.Ui.Gameplay.ClickInteract
{
    public class ClickInteractController : AUiController<ClickInteractView>
    {
        private readonly IPlayer _player;

        private IClickInteractable _clickInteractable;

        public ClickInteractController(IPlayer player)
        {
            _player = player;
        }

        public override void Initialize()
        {
            _player.ChangeClickInteract.Subscribe(OnChangeClickInteract).AddTo(View);
            _player.ClickOnInteractableObject.Subscribe(_ => OnInteractableObjectClicked()).AddTo(View);
            
            View.HideInstantly();
        }

        private void OnChangeClickInteract(IClickInteractable clickInteractable)
        {
            if (clickInteractable == null)
            {
                _clickInteractable = null;
                
                View.Disappear();
            }
            else
            {
                _clickInteractable = clickInteractable;
                
                View.ChangeProgress(clickInteractable.Progress, clickInteractable.MaxProgress);
                
                View.Appear();
            }
        }

        private void OnInteractableObjectClicked()
        {
            if (_clickInteractable == null)
                return;
            
            View.ChangeProgress(_clickInteractable.Progress, _clickInteractable.MaxProgress);
        }
    }
}