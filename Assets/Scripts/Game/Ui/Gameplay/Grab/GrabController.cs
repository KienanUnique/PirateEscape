using Game.Views.Player;
using KoboldUi.Element.Controller;
using R3;

namespace Game.Ui.Gameplay.Grab
{
    public class GrabController : AUiController<GrabView>
    {
        private readonly IPlayer _player;

        public GrabController(IPlayer player)
        {
            _player = player;
        }

        public override void Initialize()
        {
            _player.CanGrab.Subscribe(OnCanGrab).AddTo(View);
            View.HideInstantly();
        }

        private void OnCanGrab(bool canInteract)
        {
            if (canInteract)
                View.Appear();
            else
                View.Disappear();
        }
    }
}