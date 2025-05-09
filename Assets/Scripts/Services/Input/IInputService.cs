using R3;

namespace Services.Input
{
    public interface IInputService
    {
        Observable<Unit> AnyKeyPressPerformed { get; }
    }
}