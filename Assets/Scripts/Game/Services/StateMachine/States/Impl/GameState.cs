using Game.Timer;
using Game.Views.Player;
using R3;
using Services.Input;

namespace Game.Services.StateMachine.States.Impl
{
    public class GameState : AState
    {
        private readonly IPlayer _player;
        private readonly IInputService _inputService;
        private readonly ITimerService _timerService;

        public GameState(
            IPlayer player,
            IInputService inputService,
            ITimerService timerService
        )
        {
            _player = player;
            _inputService = inputService;
            _timerService = timerService;
        }

        protected override void HandleEnter()
        {
            _inputService.SwitchToGameInput();
            _player.EnableMovement();
            _timerService.StartLoseTimer();

            _timerService.TimerEnded.Subscribe(_ => OnTimerEnd()).AddTo(ActiveDisposable);
        }

        private void OnTimerEnd()
        {
            GameStateMachine.Enter<LoseState>();
        }

        protected override void HandleExit()
        {
            _timerService.StopLoseTimer();
            _inputService.SwitchToGameInput();
            _player.DisableMovement();
        }
    }
}