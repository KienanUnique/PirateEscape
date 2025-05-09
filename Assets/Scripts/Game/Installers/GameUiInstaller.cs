using Game.Ui.Dialog;
using Game.Ui.Gameplay;
using KoboldUi.Utils;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(GameUiInstaller), fileName = nameof(GameUiInstaller))]
    public class GameUiInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;

        [Header("Windows")]
        [SerializeField] private GameplayWindow _gameplayWindow;
        [SerializeField] private DialogWindow _dialogWindow;
        
        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);
            
            Container.BindWindowFromPrefab(canvasInstance, _gameplayWindow);
            Container.BindWindowFromPrefab(canvasInstance, _dialogWindow);
        }
    }
}