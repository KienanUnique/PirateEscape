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

        public ReadOnlyReactiveProperty<bool> CanInteract => _playerInteractor.CanInteract;

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