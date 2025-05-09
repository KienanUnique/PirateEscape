using Game.Views.Player;
using Services.Input;

namespace Game.Services.StateMachine.States.Impl
{
    public class GameState : AState
    {
        private readonly IPlayer _player;
        private readonly IInputService _inputService;

        public GameState(
            IPlayer player,
            IInputService inputService
        )
        {
            _player = player;
            _inputService = inputService;
        }

        protected override void HandleEnter()
        {
            _inputService.SwitchToGameInput();
            _player.EnableMovement();
        }

        protected override void HandleExit()
        {
        }
    }
}