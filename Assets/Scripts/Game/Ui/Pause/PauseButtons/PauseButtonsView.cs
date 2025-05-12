using KoboldUi.Element.View;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Pause.PauseButtons
{
    public class PauseButtonsView : AUiAnimatedView
    {
        [field: SerializeField] public Button ContinueButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }
    }
}