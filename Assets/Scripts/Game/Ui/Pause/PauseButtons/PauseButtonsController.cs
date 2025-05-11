using Game.Services.Pause;
using KoboldUi.Element.Controller;
using R3;
using UnityEngine;

namespace Game.Ui.Pause.PauseButtons
{
    public class PauseButtonsController : AUiController<PauseButtonsView>
    {
        private readonly IPauseService _pauseService;

        public PauseButtonsController(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public override void Initialize()
        {
            View.ContinueButton.OnClickAsObservable().Subscribe(_ => OnContinueButtonClicked()).AddTo(View);
            View.ExitButton.OnClickAsObservable().Subscribe(_ => OnExitButtonClicked()).AddTo(View);
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

        private void OnContinueButtonClicked()
        {
            if (!_pauseService.IsPaused.CurrentValue) 
                return;
            
            _pauseService.Unpause();
        }
    }
}