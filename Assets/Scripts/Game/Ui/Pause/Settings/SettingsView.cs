using KoboldUi.Element.View;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Pause.Settings
{
    public class SettingsView : AUiAnimatedView
    {
        [field: SerializeField] public Slider MasterVolume { get; private set; }
        [field: SerializeField] public Slider MouseSensitivity { get; private set; }
    }
}