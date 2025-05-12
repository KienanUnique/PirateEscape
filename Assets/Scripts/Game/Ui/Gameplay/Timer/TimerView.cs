using KoboldUi.Element.View;
using TMPro;
using UnityEngine;

namespace Game.Ui.Gameplay.Timer
{
    public class TimerView : AUiAnimatedView
    {
        [field: SerializeField] public TMP_Text TimerText { get; private set; }

        [Header("Timer End Animation")]
        [field: SerializeField] public int CountOfBlinks { get; private set; } = 2;
        [field: SerializeField] public float BlinkVisibleDelay { get; private set; } = 0.5f;
        [field: SerializeField] public Color TimerEndColor { get; private set; } = Color.red;
    }
}