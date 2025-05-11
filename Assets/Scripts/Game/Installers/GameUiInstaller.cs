using Game.Ui.Comics;
using Game.Ui.Dialog;
using Game.Ui.Gameplay;
using Game.Ui.Lose;
using Game.Ui.Pause;
using Game.Ui.Tutorial;
using Game.Ui.Win;
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
        [SerializeField] private WinWindow _winWindow;
        [SerializeField] private LoseWindow _loseWindow;
        [SerializeField] private ComicsWindow _comicsWindow;
        [SerializeField] private TutorialWindow _tutorialWindow;
        [SerializeField] private PauseWindow _pauseWindow;
        
        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);
            
            Container.BindWindowFromPrefab(canvasInstance, _gameplayWindow);
            Container.BindWindowFromPrefab(canvasInstance, _dialogWindow);
            Container.BindWindowFromPrefab(canvasInstance, _winWindow);
            Container.BindWindowFromPrefab(canvasInstance, _loseWindow);
            Container.BindWindowFromPrefab(canvasInstance, _comicsWindow);
            Container.BindWindowFromPrefab(canvasInstance, _tutorialWindow);
            Container.BindWindowFromPrefab(canvasInstance, _pauseWindow);
        }
    }
}