using Cysharp.Threading.Tasks;
using KoboldUi.Element.Controller;
using R3;
using Services.Scenes;
using UnityEngine;

namespace Ui.LoadingWindow.LoadingIndicator
{
    public class LoadingIndicatorController : AUiController<LoadingIndicatorView>
    {
        private readonly IScenesService _levelsService;

        public LoadingIndicatorController(IScenesService levelsService)
        {
            _levelsService = levelsService;
        }

        public override void Initialize()
        {
            var cancellationToken = View.gameObject.GetCancellationTokenOnDestroy();
            _levelsService.LoadingProgress.Subscribe(OnLoadingProgress).AddTo(cancellationToken);
        }

        private void OnLoadingProgress(float progress)
        {
            var progressPercentage = (int) (progress * 100f);
            progressPercentage = Mathf.Clamp(progressPercentage, 0, 100);

            View.loadingProgressText.text = $"{progressPercentage}%";
        }
    }
}