using Game.Ui.Win.AnyKey;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.Win
{
    public class WinWindow : AWindow
    {
        [SerializeField] private WinPressAnyKeyView _winPressAnyKeyView;

        protected override void AddControllers()
        {
            AddController<WinPressAnyKeyController, WinPressAnyKeyView>(_winPressAnyKeyView);
        }
    }
}