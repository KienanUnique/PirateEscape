using Game.Services.Dialog;
using Game.Timer;
using Game.Ui.Dialog;
using Game.Ui.Gameplay;
using Game.Views.Player;
using Game.Views.WinTrigger;
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
        private readonly IWinTriggerView _winTriggerView;

        public GameState(
            IPlayer player,
            IInputService inputService,
            ITimerService timerService,
            IDialogService dialogService,
            ILocalWindowsService localWindowsService,
            IWinTriggerView winTriggerView
        )
        {
            _player = player;
            _inputService = inputService;
            _timerService = timerService;
            _dialogService = dialogService;
            _localWindowsService = localWindowsService;
            _winTriggerView = winTriggerView;
        }

        protected override void HandleEnter()
        {
            _localWindowsService.OpenWindow<GameplayWindow>();
            
            _inputService.SwitchToGameInput();
            _player.EnableActions();
            _timerService.StartLoseTimer();

            _timerService.TimerEnded.Subscribe(_ => Lose()).AddTo(ActiveDisposable);
            _winTriggerView.PlayerEntered.Subscribe(_ => Win()).AddTo(ActiveDisposable);
            _dialogService.WinRequested.Subscribe(_ => Win()).AddTo(ActiveDisposable);
            _dialogService.LoseRequested.Subscribe(_ => Lose()).AddTo(ActiveDisposable);
            
            _dialogService.DialogStarted.Subscribe(_ => OnDialogStarted()).AddTo(ActiveDisposable);
            _dialogService.DialogComplete.Subscribe(_ => OnDialogComplete()).AddTo(ActiveDisposable);
        }

        protected override void HandleExit()
        {
            _timerService.StopLoseTimer();
            _player.DisableActions();
        }

        private void Win()
        {
            GameStateMachine.Enter<WinState>();
        }

        private void Lose()
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