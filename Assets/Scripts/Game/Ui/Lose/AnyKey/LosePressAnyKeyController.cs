using Game.Ui.Abstract.AnyKey;
using Services.Scenes;

namespace Game.Ui.Lose.AnyKey
{
    public class LosePressAnyKeyController : APressAnyKeyController<LosePressAnyKeyView>
    {
        private readonly IScenesService _scenesService;

        public LosePressAnyKeyController(IScenesService scenesService)
        {
            _scenesService = scenesService;
        }

        protected override void OnAnyKeyPressed()
        {
            _scenesService.LoadGameScene();
        }
    }
}