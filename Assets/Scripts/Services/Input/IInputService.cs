using R3;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        Observable<Unit> AnyKeyPressPerformed { get; }
        Vector2 MoveDirection { get; }
        Observable<Unit> PausePressed { get; }
        void SwitchToGameInput();
        void SwitchToUiAnyKeyInput();
    }
}