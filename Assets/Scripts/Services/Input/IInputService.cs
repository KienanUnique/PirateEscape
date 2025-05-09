using R3;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        Observable<Unit> AnyKeyPressPerformed { get; }
        Vector2 MoveDirection { get; }
        Vector2 MouseLook { get; }
        Observable<Unit> JumpPressed { get; }
        Observable<Unit> PausePressed { get; }
        void SwitchToGameInput();
        void SwitchToUiAnyKeyInput();
    }
}