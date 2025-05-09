using R3;

namespace Services.Scenes
{
    public interface IScenesService
    {
        ReadOnlyReactiveProperty<float> LoadingProgress { get; }
        ReadOnlyReactiveProperty<bool> IsLoadingCompleted { get; }
        
        void LoadGameScene();
    }
}