using Cysharp.Threading.Tasks;

namespace Game.Tutorial.Steps
{
    public interface ITutorialStep
    {
        UniTask Execute();
        void Cancel();
        bool IsComplete { get; }
    }
}