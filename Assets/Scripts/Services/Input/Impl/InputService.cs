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
        private readonly ReactiveCommand _interactionPerformed = new();
        private readonly ReactiveCommand _grabPerformed = new();
        private readonly ReactiveCommand _clickInteractPerformed = new();
        private readonly ReactiveCommand _pausePerformed = new();
        private readonly ReactiveCommand _shoutPerformed = new();
        private readonly ReactiveCommand _uiExitPerformed = new();
        private readonly ReactiveProperty<bool> _isSprintPressed = new();
        private readonly MainControls _mainControls = new();

        public Observable<Unit> AnyKeyPressPerformed => _anyKeyPressPerformed;
        public Observable<Unit> InteractionPerformed => _interactionPerformed;
        public Observable<Unit> GrabPerformed => _grabPerformed;
        public Observable<Unit> ClickInteractPerformed => _clickInteractPerformed;
        public ReadOnlyReactiveProperty<bool> IsSprintPressed => _isSprintPressed;
        public Vector2 MoveDirection => _mainControls.Gameplay.Move.ReadValue<Vector2>();
        public Vector2 MouseLook => _mainControls.Gameplay.Look.ReadValue<Vector2>();
        public Observable<Unit> JumpPressed => _jumpPerformed;
        public Observable<Unit> PausePressed => _pausePerformed;
        public Observable<Unit> UiExitPressed => _uiExitPerformed;
        public Observable<Unit> ShoutPerformed => _shoutPerformed;

        public void Initialize()
        {
            _mainControls.UiAnyKey.ButtonPressed.performed += OnAnyKeyPerformed;
            
            _mainControls.Ui.Exit.performed += OnUiExitPerformed;
            
            _mainControls.Gameplay.Pause.performed += OnPausePerformed;
            _mainControls.Gameplay.Jump.performed += OnJumpPerformed;
            _mainControls.Gameplay.Interaction.performed += OnInteractionPerformed;
            _mainControls.Gameplay.Grab.performed += OnGrabPerformed;
            _mainControls.Gameplay.ClickInteract.performed += OnClickInteractPerformed;
            _mainControls.Gameplay.Sprint.started += OnSprintStarted;
            _mainControls.Gameplay.Sprint.canceled += OnSprintCanceled;
            _mainControls.Gameplay.Shout.performed += OnShoutPerformed;
            
            SwitchToUiAnyKeyInput();
        }

        public void SwitchToGameInput()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _mainControls.UiAnyKey.Disable();
            _mainControls.Ui.Disable();
            _mainControls.Gameplay.Enable();
        }

        public void SwitchToUiAnyKeyInput()
        {
            Cursor.lockState = CursorLockMode.Confined;
            _mainControls.Gameplay.Disable();
            _mainControls.Ui.Disable();
            _mainControls.UiAnyKey.Enable();
        }

        public void SwitchToUiInput()
        {
            Cursor.lockState = CursorLockMode.Confined;

            _mainControls.UiAnyKey.Disable();
            _mainControls.Gameplay.Disable();
            _mainControls.Ui.Enable();
        }

        public void Dispose()
        {
            _anyKeyPressPerformed?.Dispose();
            
            _mainControls.UiAnyKey.ButtonPressed.performed -= OnAnyKeyPerformed;
            
            _mainControls.Ui.Exit.performed -= OnUiExitPerformed;
            
            _mainControls.Gameplay.Pause.performed -= OnPausePerformed;
            _mainControls.Gameplay.Jump.performed -= OnJumpPerformed;
            _mainControls.Gameplay.Interaction.performed -= OnInteractionPerformed;
            _mainControls.Gameplay.Grab.performed -= OnGrabPerformed;
            _mainControls.Gameplay.ClickInteract.performed -= OnClickInteractPerformed;
            _mainControls.Gameplay.Sprint.started -= OnSprintStarted;
            _mainControls.Gameplay.Sprint.canceled -= OnSprintCanceled;
            _mainControls.Gameplay.Shout.performed -= OnShoutPerformed;
            _mainControls?.Dispose();
        }

        private void OnJumpPerformed(InputAction.CallbackContext obj) => _jumpPerformed.Execute(Unit.Default);
        private void OnAnyKeyPerformed(InputAction.CallbackContext obj) => _anyKeyPressPerformed.Execute(Unit.Default);
        private void OnPausePerformed(InputAction.CallbackContext obj) => _pausePerformed.Execute(Unit.Default);
        private void OnSprintStarted(InputAction.CallbackContext obj) => _isSprintPressed.Value = true;
        private void OnSprintCanceled(InputAction.CallbackContext obj) => _isSprintPressed.Value = false;
        private void OnInteractionPerformed(InputAction.CallbackContext obj) => _interactionPerformed.Execute(Unit.Default);
        private void OnGrabPerformed(InputAction.CallbackContext obj) => _grabPerformed.Execute(Unit.Default);
        private void OnShoutPerformed(InputAction.CallbackContext obj) => _shoutPerformed.Execute(Unit.Default);
        private void OnClickInteractPerformed(InputAction.CallbackContext obj) => _clickInteractPerformed.Execute(Unit.Default);
        private void OnUiExitPerformed(InputAction.CallbackContext obj) => _uiExitPerformed.Execute(Unit.Default);
    }
}