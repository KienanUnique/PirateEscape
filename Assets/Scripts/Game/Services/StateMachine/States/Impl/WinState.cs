using Services.Scenes;

namespace Game.Services.StateMachine.States.Impl
{
    public class WinState : AState
    {
        private readonly IScenesService _scenesService;

        public WinState(IScenesService scenesService)
        {
            _scenesService = scenesService;
        }

        protected override void HandleEnter()
        {
            _scenesService.LoadTitlesScene();
            // _inputService.SwitchToUiAnyKeyInput();
            // _localWindowsService.OpenWindow<WinWindow>();
        }

        protected override void HandleExit()
        {
        }
    }
}