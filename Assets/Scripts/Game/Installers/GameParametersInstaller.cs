using Game.Db.Player;
using Game.Db.Player.Impl;
using Game.Db.Timer;
using Game.Db.Timer.Impl;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(GameParametersInstaller),
        fileName = nameof(GameParametersInstaller))]
    public class GameParametersInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerParameters _playerParameters;
        [SerializeField] private TimerParameters _timerParameters;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerParameters>().FromInstance(_playerParameters).AsSingle();
            Container.Bind<ITimerParameters>().FromInstance(_timerParameters).AsSingle();
        }
    }
}