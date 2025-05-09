using Game.Core;
using Game.Views.Player.Movement;
using UnityEngine;

namespace Game.Views.Player
{
    public class PlayerView : AView, IPlayer
    {
        [SerializeField] private PlayerMovement _playerMovement;
        
        public void EnableMovement()
        {
            _playerMovement.EnableMovement();
        }

        public void DisableMovement()
        {
            _playerMovement.DisableMovement();
        }
    }
}