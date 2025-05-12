using Game.Ui.Gameplay.ClickInteract;
using Game.Ui.Gameplay.Grab;
using Game.Ui.Gameplay.Interaction;
using Game.Ui.Gameplay.Timer;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.Gameplay
{
    public class GameplayWindow : AWindow
    {
        [SerializeField] private InteractionView _interactionView;
        [SerializeField] private GrabView _grabView;
        [SerializeField] private ClickInteractView _clickInteractView;
        [SerializeField] private TimerView _timerView;
        
        protected override void AddControllers()
        {
            AddController<InteractionController, InteractionView>(_interactionView);
            AddController<GrabController, GrabView>(_grabView);
            AddController<ClickInteractController, ClickInteractView>(_clickInteractView);
            AddController<TimerController, TimerView>(_timerView);
        }
    }
}