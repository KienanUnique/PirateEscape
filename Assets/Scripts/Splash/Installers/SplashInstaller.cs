using KoboldUi.Utils;
using Splash.Db.SplashParameters.Impl;
using Splash.Services;
using Splash.Ui.SplashWindow;
using UnityEngine;
using Utils;
using Zenject;

namespace Splash.Installers
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(SplashInstaller), fileName = nameof(SplashInstaller))]
    public class SplashInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        
        [Header("Windows")] 
        [SerializeField] private SplashWindow splashWindow;
        
        [Header("Parameters")]
        [SerializeField] private SplashParameters splashParameters;

        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);
            
            Container.BindWindowFromPrefab(canvasInstance, splashWindow);

            Container.BindInterfacesTo<SplashParameters>().FromInstance(splashParameters);
            
            Container.BindInterfacesTo<SplashService>().AsSingle().NonLazy();
        }
    }
}