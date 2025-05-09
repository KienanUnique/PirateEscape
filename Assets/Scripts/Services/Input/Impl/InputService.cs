using System;
using R3;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.Input.Impl
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private readonly ReactiveCommand _anyKeyPressPerformed = new();
        private readonly MainControls _mainControls = new();

        public Observable<Unit> AnyKeyPressPerformed => _anyKeyPressPerformed;
        
        public void Initialize()
        {
            _mainControls.UiAnyKey.Enable();
            _mainControls.UiAnyKey.ButtonPressed.performed += OnAnyKeyPerformed;
        }

        private void OnAnyKeyPerformed(InputAction.CallbackContext obj)
        {
            _anyKeyPressPerformed.Execute(Unit.Default);
        }

        public void Dispose()
        {
            _anyKeyPressPerformed?.Dispose();
            
            _mainControls.UiAnyKey.ButtonPressed.performed -= OnAnyKeyPerformed;
            _mainControls?.Dispose();
        }
    }
}