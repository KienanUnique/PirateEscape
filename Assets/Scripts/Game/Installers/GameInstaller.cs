using System;
using Game.Services.Pause.Impl;
using Game.Services.StateMachine.Impl;
using Services.FmodSound.Impl.Game.Impl;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
        }

        private void BindServices()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<GameStateMachine>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PauseService>().AsSingle();
            Container.BindInterfacesTo<GameSoundFxService>().AsSingle();
        }
    }
}