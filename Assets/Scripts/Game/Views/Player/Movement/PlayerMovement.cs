using Game.Core;
using Game.Db.Player;
using Game.Utils.Layers;
using R3;
using Services.FmodSound.Impl.Game;
using Services.FmodSound.Utils;
using Services.Input;
using Services.Settings;
using UnityEngine;
using Zenject;

namespace Game.Views.Player.Movement
{
    public class PlayerMovement : AModule
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _groundCheckPoint;
        
        [Inject] private IInputService _inputService;
        [Inject] private IPlayerParameters _playerParameters;
        [Inject] private ISettingsStorageService _settingsStorageService;
        [Inject] private IGameSoundFxService _gameSoundFxService;
        
        private bool _isInputEnabled;
        private Vector3 _moveDirection;
        private float _xRotation;
        private bool _isGrounded;
        private float _jumpCooldownTimer;
        private float _footstepCooldownTimer;
        
        private RaycastHit _slopeHit;

        public override void Initialize()
        {
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ => OnFixedUpdate()).AddTo(this);
            Observable.EveryUpdate(UnityFrameProvider.Update).Subscribe(_ => OnUpdate()).AddTo(this);

            _inputService.JumpPressed.Subscribe(_ => TryJump()).AddTo(this);
        }

        public void EnableMovement() => _isInputEnabled = true;
        public void DisableMovement() => _isInputEnabled = false;

        private void OnUpdate()
        {
            HandleMouseLook();
        }
        
        private void OnFixedUpdate()
        {
            UpdateMoveDirection();
            CheckGrounded();
            HandleMovement();
            HandleDrag();
            HandleJumpCooldown();
            TryPlayFootstepSound();
        }
        
        private void TryJump()
        {
            if (!_isInputEnabled || !_isGrounded || _jumpCooldownTimer > 0) 
                return;
            
            _rigidbody.AddForce(Vector3.up * _playerParameters.JumpForce, ForceMode.Impulse);
            _jumpCooldownTimer = _playerParameters.JumpCooldown;
        }

        private void HandleMouseLook()
        {
            if (!_isInputEnabled)
                return;
            
            var mouseLook = _inputService.MouseLook;
            var mouseX = mouseLook.x * _settingsStorageService.MouseSensitivity * Time.deltaTime;
            var mouseY = mouseLook.y * _settingsStorageService.MouseSensitivity * Time.deltaTime;
            
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -_playerParameters.LookVerticalClamp, _playerParameters.LookVerticalClamp);
            
            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerTransform.Rotate(Vector3.up * mouseX);
        }

        private void UpdateMoveDirection()
        {
            if (!_isInputEnabled)
            {
                _moveDirection = Vector3.zero;
                return;
            }

            var input = _inputService.MoveDirection;

            var forward = _cameraTransform.forward;
            var right = _cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            _moveDirection = (forward * input.y + right * input.x).normalized;
        }

        private void CheckGrounded()
        {
            _isGrounded = Physics.CheckSphere(_groundCheckPoint.position, _playerParameters.GroundCheckSphereRadius,
                LayerMasks.Ground, QueryTriggerInteraction.Ignore);
        }

        private void HandleMovement()
        {
            var speed = _inputService.IsSprintPressed.CurrentValue
                ? _playerParameters.SprintSpeed
                : _playerParameters.Speed;
            
            var targetVelocity = _moveDirection * speed;
            
            ApplyMovementForce(targetVelocity);
        }

        private void ApplyMovementForce(Vector3 targetVelocity)
        {
            var velocityChange = (targetVelocity - _rigidbody.linearVelocity);
            velocityChange.y = 0;

            var acceleration = _inputService.IsSprintPressed.CurrentValue
                ? _playerParameters.SprintAcceleration
                : _playerParameters.Acceleration;
            
            var maxAccel = acceleration * Time.fixedDeltaTime;
            velocityChange = Vector3.ClampMagnitude(velocityChange, maxAccel);

            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        private void HandleDrag()
        {
            _rigidbody.linearDamping = _isGrounded ? _playerParameters.GroundedDrag : _playerParameters.AirborneDrag;
        }

        private void HandleJumpCooldown()
        {
            if (_jumpCooldownTimer > 0)
            {
                _jumpCooldownTimer -= Time.fixedDeltaTime;
            }
        }

        private void TryPlayFootstepSound()
        {
            if (_inputService.MoveDirection == Vector2.zero || !_isGrounded)
                return;
            
            if (_footstepCooldownTimer > 0)
            {
                _footstepCooldownTimer -= Time.fixedDeltaTime;
            }
            else
            {
                _gameSoundFxService.PlaySound(EGameSoundFxType.Footstep);
                _footstepCooldownTimer = _inputService.IsSprintPressed.CurrentValue
                    ? _playerParameters.SprintFootstepCooldown
                    : _playerParameters.FootstepCooldown;
            }
        }
    }
}