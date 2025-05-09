using Services.Input;

namespace Game.Services.StateMachine.States.Impl
{
    public class LoseState : AState
    {
        private readonly IInputService _inputService;

        public LoseState(IInputService inputService)
        {
            _inputService = inputService;
        }

        protected override void HandleEnter()
        {
            _inputService.SwitchToUiInput();
        }

        protected override void HandleExit()
        {
            throw new System.NotImplementedException();
        }
    }
}