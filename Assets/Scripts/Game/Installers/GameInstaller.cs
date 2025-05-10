using System;
using Alchemy.Inspector;
using Game.Services.CameraHolder.Impl;
using Game.Services.Dialog.Impl;
using Game.Services.NonBindedViewsInitializer;
using Game.Services.Pause.Impl;
using Game.Services.StateMachine.Impl;
using Game.Timer.Impl;
using Game.Utils.NonBindedViews;
using Game.Views.Player;
using Game.Views.Timer;
using Game.Views.WinTrigger;
using Services.FmodSound.Impl.Game.Impl;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerInstance;
        [SerializeField] private TimerView _timerInstance;
        [SerializeField] private NonBindedViewsHolder _nonBindedViewsHolder;
        [SerializeField] private Camera _cameraInstance;
        [SerializeField] private WinTriggerView _winTriggerViewInstance;
        
        public override void InstallBindings()
        {
            BindViews();
            BindServices();
        }

        private void BindViews()
        {
            Container.BindInterfacesAndSelfTo<PlayerView>().FromInstance(_playerInstance).AsSingle();
            Container.BindInterfacesAndSelfTo<TimerView>().FromInstance(_timerInstance).AsSingle();
            Container.BindInterfacesAndSelfTo<WinTriggerView>().FromInstance(_winTriggerViewInstance).AsSingle();
        }

        private void BindServices()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<GameStateMachine>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PauseService>().AsSingle();
            Container.BindInterfacesTo<GameSoundFxService>().AsSingle();
            Container.BindInterfacesTo<TimerService>().AsSingle();
            Container.BindInterfacesTo<DialogService>().AsSingle();
            Container.BindInterfacesTo<CameraHolderService>().AsSingle().WithArguments(_cameraInstance);
            Container.BindInterfacesTo<NonBindedViewsInitializerService>().AsSingle().WithArguments(_nonBindedViewsHolder).NonLazy();
        }
        
#if UNITY_EDITOR
        [Button]
        public virtual void Autofill()
        {
            _playerInstance = FindFirstObjectByType<PlayerView>();
            _timerInstance = FindFirstObjectByType<TimerView>();
            _cameraInstance = FindFirstObjectByType<Camera>();
            _nonBindedViewsHolder = FindFirstObjectByType<NonBindedViewsHolder>();
            _winTriggerViewInstance = FindFirstObjectByType<WinTriggerView>();
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}