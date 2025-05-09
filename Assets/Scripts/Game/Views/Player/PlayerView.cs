using Game.Core;
using Game.Views.Player.Interactor;
using Game.Views.Player.Movement;
using UnityEngine;

namespace Game.Views.Player
{
    public class PlayerView : AView, IPlayer
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerInteractor _playerInteractor;
        
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