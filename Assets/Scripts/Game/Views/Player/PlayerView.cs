using Game.Core;
using Game.Views.Player.Interactor;
using Game.Views.Player.Movement;
using R3;
using UnityEngine;

namespace Game.Views.Player
{
    public class PlayerView : AView, IPlayer
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerInteractor _playerInteractor;
        [SerializeField] private PlayerGrab _playerGrab;
        [SerializeField] private PlayerClickInteract _clickInteract;

        public ReadOnlyReactiveProperty<bool> CanInteract => _playerInteractor.CanInteract;
        public ReadOnlyReactiveProperty<bool> CanGrab => _playerGrab.CanGrab;
        public ReadOnlyReactiveProperty<IClickInteractable> ChangeClickInteract => _clickInteract.ChosenClickInteractable;
        public Observable<Unit> ClickOnInteractableObject => _clickInteract.ClickOnObjectInteractable;

        public void EnableActions()
        {
            _playerMovement.EnableMovement();
            _playerInteractor.EnableInteraction();
        }

        public void DisableActions()
        {
            _playerMovement.DisableMovement();
            _playerInteractor.DisableInteraction();
        }
    }
}