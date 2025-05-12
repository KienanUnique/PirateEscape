using Game.Ui.Lose.AnyKey;
using Game.Ui.Lose.Explosion;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.Lose
{
    public class LoseWindow : AWindow
    {
        [SerializeField] private LosePressAnyKeyView _losePressAnyKeyView;
        [SerializeField] private ExplosionView _explosionView;

        protected override void AddControllers()
        {
            AddController<LosePressAnyKeyController, LosePressAnyKeyView>(_losePressAnyKeyView);
            AddController<ExplosionController, ExplosionView>(_explosionView);
        }
    }
}