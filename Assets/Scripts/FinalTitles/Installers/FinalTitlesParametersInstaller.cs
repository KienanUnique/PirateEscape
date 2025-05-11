using FinalTitles.Db.Video;
using FinalTitles.Db.Video.Impl;
using UnityEngine;
using Utils;
using Zenject;

namespace FinalTitles.Installers
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(FinalTitlesParametersInstaller),
        fileName = nameof(FinalTitlesParametersInstaller))]
    public class FinalTitlesParametersInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private VideoBase _videoBase;

        public override void InstallBindings()
        {
            Container.Bind<IVideoBase>().FromInstance(_videoBase).AsSingle();
        }
    }
}