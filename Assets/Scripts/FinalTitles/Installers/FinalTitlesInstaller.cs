using FinalTitles.Services.FinalTitles;
using FinalTitles.Utils.Player;
using FinalTitles.Utils.Player.Impl;
using UnityEngine;
using Zenject;

namespace FinalTitles.Installers
{
    public class FinalTitlesInstaller : MonoInstaller
    {
        [SerializeField] private FinalTitlesVideoPlayer _finalTitlesVideoPlayer;

        public override void InstallBindings()
        {
            Container.Bind<IFinalTitlesVideoPlayer>().FromInstance(_finalTitlesVideoPlayer).AsSingle();
            Container.BindInterfacesAndSelfTo<FinalTitlesService>().AsSingle().NonLazy();
        }
    }
}