using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using IInitializable = Zenject.IInitializable;
using Unit = R3.Unit;

namespace Services.Input.Impl
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private readonly ReactiveCommand _anyKeyPressPerformed = new();
        private readonly ReactiveCommand _jumpPerformed = new();
        private readonly ReactiveCommand _pausePerformed = new();
        private readonly MainControls _mainControls = new();

        public Observable<Unit> AnyKeyPressPerformed => _anyKeyPressPerformed;
        public Vector2 MoveDirection => _mainControls.Gameplay.Move.ReadValue<Vector2>();
        public Vector2 MouseLook => _mainControls.Gameplay.Look.ReadValue<Vector2>();
        public Observable<Unit> JumpPressed => _jumpPerformed;
        public Observable<Unit> PausePressed => _pausePerformed;

        public void Initialize()
        {
            _mainControls.UiAnyKey.ButtonPressed.performed += OnAnyKeyPerformed;
            _mainControls.Gameplay.Pause.performed += OnPausePerformed;
            _mainControls.Gameplay.Jump.performed += OnJumpPerformed;
            
            SwitchToUiAnyKeyInput();
        }

        public void SwitchToGameInput()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _mainControls.UiAnyKey.Disable();
            _mainControls.Gameplay.Enable();
        }

        public void SwitchToUiAnyKeyInput()
        {
            Cursor.lockState = CursorLockMode.Confined;
            _mainControls.UiAnyKey.Enable();
            _mainControls.Gameplay.Disable();
        }

        public void Dispose()
        {
            _anyKeyPressPerformed?.Dispose();
            
            _mainControls.UiAnyKey.ButtonPressed.performed -= OnAnyKeyPerformed;
            _mainControls.Gameplay.Pause.performed -= OnPausePerformed;
            _mainControls.Gameplay.Jump.performed -= OnJumpPerformed;
            _mainControls?.Dispose();
        }

        private void OnJumpPerformed(InputAction.CallbackContext obj)
        {
            _jumpPerformed.Execute(Unit.Default);
        }

        private void OnAnyKeyPerformed(InputAction.CallbackContext obj)
        {
            _anyKeyPressPerformed.Execute(Unit.Default);
        }
        
        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            _pausePerformed.Execute(Unit.Default);
        }
    }
}