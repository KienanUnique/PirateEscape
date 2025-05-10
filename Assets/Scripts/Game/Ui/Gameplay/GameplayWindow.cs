using Game.Ui.Gameplay.Grab;
using Game.Ui.Gameplay.Interaction;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.Gameplay
{
    public class GameplayWindow : AWindow
    {
        [SerializeField] private InteractionView _interactionView;
        [SerializeField] private GrabView _grabView;
        
        protected override void AddControllers()
        {
            AddController<InteractionController, InteractionView>(_interactionView);
            AddController<GrabController, GrabView>(_grabView);
        }
    }
}