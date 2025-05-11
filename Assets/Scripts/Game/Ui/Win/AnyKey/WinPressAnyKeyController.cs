using Game.Ui.Abstract.AnyKey;
using Services.Scenes;

namespace Game.Ui.Win.AnyKey
{
    public class WinPressAnyKeyController : APressAnyKeyController<WinPressAnyKeyView>
    {
        private readonly IScenesService _scenesService;

        public WinPressAnyKeyController(IScenesService scenesService)
        {
            _scenesService = scenesService;
        }

        protected override void OnAnyKeyPressed()
        {
            _scenesService.LoadTitlesScene();
        }
    }
}