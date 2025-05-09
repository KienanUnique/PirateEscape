using Game.Core;
using Game.Db.Player;
using Services.Input;
using Zenject;

namespace Game.Views.Player.Movement.Impl
{
    
    public class PlayerMovement : AModule, IPlayerMovement
    {
        [Inject] private IInputService _inputService;
        [Inject] private IPlayerParameters _playerParameters;
        
        private bool _isInputEnabled;
        
        public void EnableMovement()
        {
            _isInputEnabled = true;
        }

        public void DisableMovement()
        {
            _isInputEnabled = false;
        }

        private void Update()
        {
            // TODO: add physics movement
        }
    }
}