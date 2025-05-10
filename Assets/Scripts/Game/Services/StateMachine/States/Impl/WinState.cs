using Game.Ui.Win;
using KoboldUi.Services.WindowsService;
using Services.Input;

namespace Game.Services.StateMachine.States.Impl
{
    public class WinState : AState
    {
        private readonly IInputService _inputService;
        private readonly ILocalWindowsService _localWindowsService;

        public WinState(
            IInputService inputService,
            ILocalWindowsService localWindowsService
        )
        {
            _inputService = inputService;
            _localWindowsService = localWindowsService;
        }

        protected override void HandleEnter()
        {
            _inputService.SwitchToUiAnyKeyInput();
            _localWindowsService.OpenWindow<WinWindow>();
        }

        protected override void HandleExit()
        {
        }
    }
}