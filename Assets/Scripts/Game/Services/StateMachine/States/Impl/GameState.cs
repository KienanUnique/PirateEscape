using Game.Services.Dialog;
using Game.Timer;
using Game.Ui.Dialog;
using Game.Ui.Gameplay;
using Game.Views.Player;
using KoboldUi.Services.WindowsService;
using R3;
using Services.Input;

namespace Game.Services.StateMachine.States.Impl
{
    public class GameState : AState
    {
        private readonly IPlayer _player;
        private readonly IInputService _inputService;
        private readonly ITimerService _timerService;
        private readonly IDialogService _dialogService;
        private readonly ILocalWindowsService _localWindowsService;

        public GameState(
            IPlayer player,
            IInputService inputService,
            ITimerService timerService,
            IDialogService dialogService,
            ILocalWindowsService localWindowsService
        )
        {
            _player = player;
            _inputService = inputService;
            _timerService = timerService;
            _dialogService = dialogService;
            _localWindowsService = localWindowsService;
        }

        protected override void HandleEnter()
        {
            _localWindowsService.OpenWindow<GameplayWindow>();
            
            _inputService.SwitchToGameInput();
            _player.EnableActions();
            _timerService.StartLoseTimer();

            _timerService.TimerEnded.Subscribe(_ => OnTimerEnd()).AddTo(ActiveDisposable);
            _dialogService.DialogStarted.Subscribe(_ => OnDialogStarted()).AddTo(ActiveDisposable);
            _dialogService.DialogComplete.Subscribe(_ => OnDialogComplete()).AddTo(ActiveDisposable);
        }

        protected override void HandleExit()
        {
            _timerService.StopLoseTimer();
            _inputService.SwitchToGameInput();
            _player.DisableActions();
        }

        private void OnTimerEnd()
        {
            GameStateMachine.Enter<LoseState>();
        }

        private void OnDialogStarted()
        {
            _localWindowsService.OpenWindow<DialogWindow>();
            _player.DisableActions();
            _inputService.SwitchToUiInput();
        }
        
        private void OnDialogComplete()
        {
            _localWindowsService.TryBackWindow();
            _player.EnableActions();
            _inputService.SwitchToGameInput();
        }
    }
}