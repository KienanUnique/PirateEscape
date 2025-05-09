using System;
using R3;
using UnityEngine;

namespace Utils
{
    public class LoadingProgressHandler : IDisposable
    {
        private readonly ReactiveProperty<float> _loadingProgress = new();
        private readonly ReactiveCommand _loadingCompleted = new();

        private AsyncOperation _localSceneLoadingOperation;
        private IDisposable _loadingProgressDisposable;

        public Observable<Unit> LoadingCompleted => _loadingCompleted;
        public ReadOnlyReactiveProperty<float> LoadingProgress => _loadingProgress;

        public void SetLocalSceneLoading(AsyncOperation localSceneLoadingOperation)
        {
            _localSceneLoadingOperation = localSceneLoadingOperation;
            _loadingProgress.Value = 0f;
            StartCheckingLoadingProgress();

            _localSceneLoadingOperation.completed += OnLoadingCompleted;
        }

        private void OnLoadingCompleted(AsyncOperation operation)
        {
            _localSceneLoadingOperation.completed -= OnLoadingCompleted;
            _localSceneLoadingOperation = null;
            HandleLoadingComplete();
        }

        private void HandleLoadingComplete()
        {
            _loadingProgressDisposable?.Dispose();
            _loadingProgress.Value = 1f;
            _loadingCompleted.Execute(Unit.Default);
        }

        public void Dispose()
        {
            _loadingCompleted.Dispose();
            _loadingProgress.Dispose();
            _loadingProgressDisposable?.Dispose();
        }

        private void StartCheckingLoadingProgress()
        {
            _loadingProgressDisposable?.Dispose();
            
            CheckLoadingProgress();
            _loadingProgressDisposable = Observable.EveryUpdate().Subscribe(_ => CheckLoadingProgress());
        }

        private void CheckLoadingProgress()
        {
            _loadingProgress.Value = _localSceneLoadingOperation.progress;
        }
    }
}