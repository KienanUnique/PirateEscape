using Cysharp.Threading.Tasks;
using Game.Ui.Comics;
using Game.Ui.Tutorial;
using KoboldUi.Services.WindowsService;
using R3;
using Services.FmodSound.Impl.Background;
using Services.Input;
using Services.Session;

namespace Game.Services.StateMachine.States.Impl
{
    public class StartState : AState
    {
        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly ILocalWindowsService _localWindowsService;
        private readonly IInputService _inputService;
        private readonly ISessionService _sessionService;

        public StartState(
            IBackgroundMusicService backgroundMusicService, 
            ILocalWindowsService localWindowsService, 
            IInputService inputService, 
            ISessionService sessionService
        )
        {
            _backgroundMusicService = backgroundMusicService;
            _localWindowsService = localWindowsService;
            _inputService = inputService;
            _sessionService = sessionService;
        }
        
        protected override void HandleEnter()
        {
            if (_sessionService.IsFirstGameStart)
            {
                StartGameWithTutorial().Forget();
                return;
            }

            StartGameWithoutTutorial();
        }

        private void StartGameWithoutTutorial()
        {
            _backgroundMusicService.Play();
            GameStateMachine.Enter<GameState>();
        }

        private async UniTask StartGameWithTutorial()
        {
            _inputService.SwitchToUiAnyKeyInput();
            
            _localWindowsService.OpenWindow<ComicsWindow>();
            await _inputService.AnyKeyPressPerformed.FirstAsync();
            
            _backgroundMusicService.Play();
            
            _localWindowsService.OpenWindow<TutorialWindow>();
            await _inputService.AnyKeyPressPerformed.FirstAsync();
            
            GameStateMachine.Enter<GameState>();
        }

        protected override void HandleExit()
        {
            _sessionService.HandleLevelStart();
        }
    }
}