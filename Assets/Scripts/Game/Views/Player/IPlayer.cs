using R3;

namespace Game.Views.Player
{
    public interface IPlayer
    {
        ReadOnlyReactiveProperty<bool> CanInteract { get; }
        ReadOnlyReactiveProperty<bool> CanGrab { get; }
        ReadOnlyReactiveProperty<IClickInteractable> ChangeClickInteract { get; }
        Observable<Unit> ClickOnInteractableObject { get; }
        
        void EnableActions();
        void DisableActions();
    }
}