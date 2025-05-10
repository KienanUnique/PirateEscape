using Services.FmodSound.Impl;
using Services.FmodSound.Impl.Background.Impl;
using Services.FmodSound.Impl.Ui.Impl;
using Services.Input.Impl;
using Services.Scenes.Impl;
using Services.Session.Impl;
using Services.Settings.Impl;
using Zenject;

namespace Installers
{
    public class ProjectServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScenesService>().AsSingle();
            
            Container.BindInterfacesTo<InputService>().AsSingle();
            
            Container.BindInterfacesTo<SettingsStorageService>().AsSingle();
            Container.BindInterfacesTo<UiSoundsService>().AsSingle();
            Container.BindInterfacesTo<BackgroundMusicService>().AsSingle();
            Container.BindInterfacesTo<GlobalSoundsService>().AsSingle();
            Container.BindInterfacesTo<SessionService>().AsSingle();
        }
    }
}