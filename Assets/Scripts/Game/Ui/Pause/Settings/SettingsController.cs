using KoboldUi.Element.Controller;
using R3;
using Services.Settings;

namespace Game.Ui.Pause.Settings
{
    public class SettingsController : AUiController<SettingsView>
    {
        private readonly ISettingsStorageService _settingsStorageService;

        private bool _isWaitingTestSoundEnd;

        public SettingsController(
            ISettingsStorageService settingsStorageService
        )
        {
            _settingsStorageService = settingsStorageService;
        }

        public override void Initialize()
        {
            View.MasterVolume.value = _settingsStorageService.MasterVolume.CurrentValue;
            View.MasterVolume.OnValueChangedAsObservable().Subscribe(OnMasterVolumeChanged).AddTo(View);
            
            View.MouseSensitivity.value = _settingsStorageService.MouseSensitivity;
            View.MouseSensitivity.OnValueChangedAsObservable().Subscribe(OnMouseSensitivityChanged).AddTo(View);
        }

        protected override void OnOpen()
        {
            View.MasterVolume.value = _settingsStorageService.MasterVolume.CurrentValue;
            View.MouseSensitivity.value = _settingsStorageService.MouseSensitivity;
        }

        private void OnMasterVolumeChanged(float volume) => _settingsStorageService.SetMasterVolume(volume);
        private void OnMouseSensitivityChanged(float sensitivity) => _settingsStorageService.SetMouseSensitivity(sensitivity);
    }
}