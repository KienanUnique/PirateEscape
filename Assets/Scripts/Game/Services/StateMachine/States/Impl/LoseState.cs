using Game.Ui.Lose;
using KoboldUi.Services.WindowsService;
using Services.FmodSound.Impl.Game;
using Services.FmodSound.Utils;
using Services.Input;

namespace Game.Services.StateMachine.States.Impl
{
    public class LoseState : AState
    {
        private readonly IInputService _inputService;
        private readonly ILocalWindowsService _localWindowsService;
        private readonly IGameSoundFxService _gameSoundFxService;

        public LoseState(
            IInputService inputService,
            ILocalWindowsService localWindowsService,
            IGameSoundFxService gameSoundFxService
        )
        {
            _inputService = inputService;
            _localWindowsService = localWindowsService;
            _gameSoundFxService = gameSoundFxService;
        }

        protected override void HandleEnter()
        {
            _gameSoundFxService.PlaySound(EGameSoundFxType.Explosion);
            _inputService.SwitchToUiAnyKeyInput();
            _localWindowsService.OpenWindow<LoseWindow>();
        }

        protected override void HandleExit()
        {
        }
    }
}