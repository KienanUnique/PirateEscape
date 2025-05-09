using System;

namespace Game.Services.StateMachine.States.Impl
{
    public class StartState : AState
    {
        public StartState()
        {
        }

        protected override void HandleEnter()
        {
            throw new NotImplementedException();
            //GameStateMachine.Enter<GameState>();
        }

        protected override void HandleExit()
        {
            throw new NotImplementedException();
        }
    }
}