using Game.Services.StateMachine.States;

namespace Game.Services.StateMachine
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : IState;
    }
}