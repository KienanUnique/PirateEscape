using Game.Ui.Gameplay.Interaction;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.Gameplay
{
    public class GameplayWindow : AWindow
    {
        [SerializeField] private InteractionView _interactionView;
        protected override void AddControllers()
        {
            AddController<InteractionController, InteractionView>(_interactionView);
        }
    }
}