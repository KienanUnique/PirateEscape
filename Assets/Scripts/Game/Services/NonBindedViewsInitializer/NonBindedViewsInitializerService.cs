using Game.Utils.NonBindedViews;
using Zenject;

namespace Game.Services.NonBindedViewsInitializer
{
    public class NonBindedViewsInitializerService : IInitializable
    {
        private readonly NonBindedViewsHolder _nonBindedViewsHolder;
        private readonly DiContainer _diContainer;

        public NonBindedViewsInitializerService(
            NonBindedViewsHolder nonBindedViewsHolder, 
            DiContainer diContainer
        )
        {
            _nonBindedViewsHolder = nonBindedViewsHolder;
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            var views = _nonBindedViewsHolder.NonBindedViews;
            foreach (var aView in views)
            {
                _diContainer.InjectGameObject(aView.gameObject);
                aView.Initialize();
            }
        }
    }
}