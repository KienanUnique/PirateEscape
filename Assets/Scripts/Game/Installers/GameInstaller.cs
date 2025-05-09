using System;
using Alchemy.Inspector;
using Game.Services.Pause.Impl;
using Game.Services.StateMachine.Impl;
using Game.Views.Player;
using Services.FmodSound.Impl.Game.Impl;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] PlayerView _playerInstance;
        
        public override void InstallBindings()
        {
            BindViews();
            BindServices();
        }

        private void BindViews()
        {
            Container.BindInterfacesAndSelfTo<PlayerView>().FromInstance(_playerInstance).AsSingle();
        }

        private void BindServices()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<GameStateMachine>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PauseService>().AsSingle();
            Container.BindInterfacesTo<GameSoundFxService>().AsSingle();
        }
        
#if UNITY_EDITOR
        [Button]
        public virtual void Autofill()
        {
            _playerInstance = FindFirstObjectByType<PlayerView>();
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}