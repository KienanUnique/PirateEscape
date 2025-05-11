using System;
using FinalTitles.Utils.Player;
using R3;
using Services.FmodSound.Impl.Background;
using Services.Scenes;
using Zenject;

namespace FinalTitles.Services.FinalTitles
{
    public class FinalTitlesService : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        
        private readonly IFinalTitlesVideoPlayer _videoPlayer;
        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly IScenesService _scenesService;

        public FinalTitlesService(
            IFinalTitlesVideoPlayer videoPlayer, 
            IBackgroundMusicService backgroundMusicService,
            IScenesService scenesService
        )
        {
            _videoPlayer = videoPlayer;
            _backgroundMusicService = backgroundMusicService;
            _scenesService = scenesService;
        }

        public void Initialize()
        {
            _backgroundMusicService.Stop();
            _videoPlayer.Play();
            
            _videoPlayer.VideoEnded.Subscribe(_ => OnVideoEnded()).AddTo(_disposables);
        }

        private void OnVideoEnded()
        {
            _scenesService.LoadGameScene();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}