using Game.Services.Dialog;
using Game.Services.Pause;
using Game.Timer;
using Game.Ui.Dialog;
using Game.Ui.Gameplay;
using Game.Ui.Pause;
using Game.Views.Player;
using Game.Views.WinTrigger;
using KoboldUi.Services.WindowsService;
using R3;
using Services.FmodSound.Impl.Ui;
using Services.FmodSound.Utils;
using Services.Input;
using Services.Session;
using Utils;

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
        private readonly IPauseService _pauseService;
        private readonly ISessionService _sessionService;
        private readonly IUiSoundFxService _uiSoundFxService;

        public GameState(
            IPlayer player,
            IInputService inputService,
            ITimerService timerService,
            IDialogService dialogService,
            ILocalWindowsService localWindowsService,
            IWinTriggerView winTriggerView,
            IPauseService pauseService,
            ISessionService sessionService,
            IUiSoundFxService uiSoundFxService
        )
        {
            _player = player;
            _inputService = inputService;
            _timerService = timerService;
            _dialogService = dialogService;
            _localWindowsService = localWindowsService;
            _winTriggerView = winTriggerView;
            _pauseService = pauseService;
            _sessionService = sessionService;
            _uiSoundFxService = uiSoundFxService;
        }

        protected override void HandleEnter()
        {
            _localWindowsService.OpenWindow<GameplayWindow>();
            
            _inputService.SwitchToGameInput();
            _player.EnableActions();
            _timerService.StartLoseTimer();

            _timerService.TimerEnded.Subscribe(_ => Lose()).AddTo(ActiveDisposable);
            _winTriggerView.WinRequested.Subscribe(Win).AddTo(ActiveDisposable);
            _dialogService.WinRequested.Subscribe(Win).AddTo(ActiveDisposable);
            _dialogService.LoseRequested.Subscribe(_ => Lose()).AddTo(ActiveDisposable);
            
            _dialogService.DialogStarted.Subscribe(_ => OnDialogStarted()).AddTo(ActiveDisposable);
            _dialogService.DialogComplete.Subscribe(_ => OnDialogComplete()).AddTo(ActiveDisposable);
            
            _pauseService.IsPaused.Subscribe(HandlePause).AddTo(ActiveDisposable);
            _inputService.PausePressed.Subscribe(_ => OnPausePressed()).AddTo(ActiveDisposable);
            _inputService.UiExitPressed.Subscribe(_ => OnExitPressed()).AddTo(ActiveDisposable);
        }

        protected override void HandleExit()
        {
            _timerService.StopLoseTimer();
            _player.DisableActions();
        }
        
        private void OnPausePressed()
        {
            if (_pauseService.IsPaused.CurrentValue)
                return;
            
            _pauseService.Pause();
        }
        
        private void OnExitPressed()
        {
            if (!_pauseService.IsPaused.CurrentValue)
                return;
            
            _pauseService.Unpause();
        }
        
        private void HandlePause(bool isPaused)
        {
            if (isPaused)
            {
                _inputService.SwitchToUiInput();
                _localWindowsService.OpenWindow<PauseWindow>();
            }
            else
            {
                _inputService.SwitchToGameInput();
                _localWindowsService.OpenWindow<GameplayWindow>();
            }
        }

        private void Win(EWinEnding ending)
        {
            _sessionService.HandleWin(ending);
            GameStateMachine.Enter<WinState>();
        }

        private void Lose()
        {
            GameStateMachine.Enter<LoseState>();
        }

        private void OnDialogStarted()
        {
            _timerService.SetPause(true);
            _localWindowsService.OpenWindow<DialogWindow>();
            _player.DisableActions();
            _inputService.SwitchToUiInput();
            _uiSoundFxService.PlaySound(EUiSoundFxType.DialogStart);
        }
        
        private void OnDialogComplete()
        {
            _timerService.SetPause(false);
            _localWindowsService.TryBackWindow();
            _player.EnableActions();
            _inputService.SwitchToGameInput();
        }
    }
}