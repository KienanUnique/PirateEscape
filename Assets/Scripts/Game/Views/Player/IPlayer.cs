using R3;

namespace Game.Views.Player
{
    public interface IPlayer
    {
        ReadOnlyReactiveProperty<bool> CanInteract { get; }
        void EnableActions();
        void DisableActions();
    }
}