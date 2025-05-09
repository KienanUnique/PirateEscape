using Game.Views.Player;
using KoboldUi.Element.Controller;
using R3;

namespace Game.Ui.Gameplay.Interaction
{
    public class InteractionController : AUiController<InteractionView>
    {
        private readonly IPlayer _player;

        public InteractionController(IPlayer player)
        {
            _player = player;
        }

        public override void Initialize()
        {
            _player.CanInteract.Subscribe(OnCanInteract).AddTo(View);
            View.HideInstantly();
        }

        private void OnCanInteract(bool canInteract)
        {
            if (canInteract)
                View.Appear();
            else
                View.Disappear();
        }
    }
}