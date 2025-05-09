using System;
using KoboldUi.Services.WindowsService;
using R3;
using Services.FmodSound.Impl.Ui;
using Services.FmodSound.Utils;
using Services.Input;
using Services.Scenes;
using Splash.Db.SplashParameters;
using Splash.Ui.SplashWindow;
using UnityEngine;
using Zenject;

namespace Splash.Services
{
    public class SplashService : IInitializable, IDisposable
    {
        private readonly IScenesService _scenesService;
        private readonly IUiSoundFxService _uiSoundFxService;
        private readonly ILocalWindowsService _localWindowsService;
        private readonly ISplashParameters _splashParameters;
        private readonly IInputService _inputService;
        
        private readonly CompositeDisposable _compositeDisposable = new();

        public SplashService(
            IScenesService scenesService,
            IUiSoundFxService uiSoundFxService,
            ILocalWindowsService localWindowsService,
            ISplashParameters splashParameters,
            IInputService inputService
        )
        {
            _scenesService = scenesService;
            _uiSoundFxService = uiSoundFxService;
            _localWindowsService = localWindowsService;
            _splashParameters = splashParameters;
            _inputService = inputService;
        }
        
        public void Initialize()
        {
            _localWindowsService.OpenWindow<SplashWindow>();
            
            _uiSoundFxService.PlaySound(
                soundType: EUiSoundFxType.KitchenInTheDungeon,
                onSoundFinished: () => _localWindowsService.TryBackWindow()
            );
            
            _inputService.AnyKeyPressPerformed.Subscribe(_ => OnAnyKeyPressed()).AddTo(_compositeDisposable);
            
            
            Observable.Timer(TimeSpan.FromSeconds(_splashParameters.SplashDuration))
                .Subscribe(_ => HandleSplashEnd())
                .AddTo(_compositeDisposable);
            
            Observable.Timer(TimeSpan.FromSeconds(_splashParameters.CloseLogoDelay))
                .Subscribe(_ => _localWindowsService.TryBackWindow())
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private void OnAnyKeyPressed()
        {
            _localWindowsService.TryBackWindow();
            HandleSplashEnd();
        }

        private void HandleSplashEnd()
        {
            _uiSoundFxService.Stop(EUiSoundFxType.KitchenInTheDungeon);
            _scenesService.LoadNextScene();
            
            _compositeDisposable?.Dispose();
        }
    }
}