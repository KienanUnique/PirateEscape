using System;
using KoboldUi.Services.WindowsService;
using KoboldUi.Utils;
using R3;
using Ui.LoadingWindow;
using UnityEngine.SceneManagement;
using Utils;

namespace Services.Scenes.Impl
{
    public class ScenesService : IScenesService, IDisposable
    {
        private const string GameSceneName = "GameScene";
        private const string TitlesSceneName = "TitlesScene";

        private readonly IProjectWindowsService _projectWindowsService;
        
        private readonly ReactiveProperty<bool> _isLoadingCompleted = new(true);

        private readonly LoadingProgressHandler _loadingProgressHandler = new();

        public ReadOnlyReactiveProperty<float> LoadingProgress => _loadingProgressHandler.LoadingProgress;
        public ReadOnlyReactiveProperty<bool> IsLoadingCompleted => _isLoadingCompleted;

        public ScenesService(IProjectWindowsService projectWindowsService)
        {
            _projectWindowsService = projectWindowsService;
        }

        public void LoadGameScene()
        {
            LoadScene(GameSceneName);
        }

        public void LoadTitlesScene()
        {
            LoadScene(TitlesSceneName);
        }

        public void Dispose()
        {
            _loadingProgressHandler.Dispose();
        }
        
        private void LoadScene(string sceneName)
        {
            if(!_isLoadingCompleted.Value)
                return;

            _isLoadingCompleted.Value = false;

            _projectWindowsService.OpenWindow<LoadingWindow>(previousWindowPolitic: EAnimationPolitic.DoNotWait);
            
            var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            
            _loadingProgressHandler.SetLocalSceneLoading(loadingOperation);
            _loadingProgressHandler.LoadingCompleted.Subscribe(_ => OnLoadingCompleted());
        }

        private void OnLoadingCompleted()
        {
            _projectWindowsService.TryBackWindow(previousWindowPolitic: EAnimationPolitic.DoNotWait);
            _isLoadingCompleted.Value = true;
        }
    }
}