using Db.Sounds;
using Db.Sounds.Impl;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(ProjectParametersInstaller),
        fileName = nameof(ProjectParametersInstaller))]
    public class ProjectParametersInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private SoundFxBase soundFxBase;
        [SerializeField] private GameDefaultParameters gameDefaultParameters;
        [SerializeField] private SoundParameters soundParameters;
        
        public override void InstallBindings()
        {
            Container.Bind<ISoundFxBase>().FromInstance(soundFxBase).AsSingle();
            Container.Bind<IGameDefaultParameters>().FromInstance(gameDefaultParameters).AsSingle();
            Container.Bind<ISoundParameters>().FromInstance(soundParameters).AsSingle();
        }
    }
}