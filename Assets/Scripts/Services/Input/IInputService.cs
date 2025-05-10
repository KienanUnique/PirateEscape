using R3;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        Observable<Unit> AnyKeyPressPerformed { get; }
        Observable<Unit> InteractionPerformed { get; }
        Observable<Unit> GrabPerformed { get; }
        Observable<Unit> ClickInteractPerformed { get; }
        ReadOnlyReactiveProperty<bool> IsSprintPressed { get; }
        Vector2 MoveDirection { get; }
        Vector2 MouseLook { get; }
        Observable<Unit> JumpPressed { get; }
        Observable<Unit> PausePressed { get; }
        void SwitchToGameInput();
        void SwitchToUiAnyKeyInput();
        void SwitchToUiInput();
    }
}