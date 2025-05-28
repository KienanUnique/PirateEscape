using System;
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
        
        Observable<Unit> OnMovementStarted { get; }
        Observable<Unit> OnJumped { get; }
        Observable<Unit> OnObjectInteracted { get; }
        bool IsInInteractionZone { get; }
        Observable<Unit> OnDied { get; }
    }
}