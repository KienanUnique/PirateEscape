using Game.Ui.Pause.PauseButtons;
using Game.Ui.Pause.Settings;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.Pause
{
    public class PauseWindow : AWindow
    {
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private PauseButtonsView _pauseButtonsView;
        
        protected override void AddControllers()
        {
            AddController<SettingsController, SettingsView>(_settingsView);
            AddController<PauseButtonsController, PauseButtonsView>(_pauseButtonsView);
        }
    }
}