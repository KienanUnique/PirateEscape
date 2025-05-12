using System;
using FinalTitles.Db.Video;
using FinalTitles.Utils.Player;
using R3;
using Services.FmodSound.Impl.Background;
using Services.Scenes;
using Services.Session;
using Utils;
using Zenject;

namespace FinalTitles.Services.FinalTitles
{
    public class FinalTitlesService : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        
        private readonly IFinalTitlesVideoPlayer _videoPlayer;
        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly IScenesService _scenesService;
        private readonly IVideoBase _videoBase;
        private readonly ISessionService _sessionService;

        public FinalTitlesService(
            IFinalTitlesVideoPlayer videoPlayer, 
            IBackgroundMusicService backgroundMusicService,
            IScenesService scenesService,
            IVideoBase videoBase,
            ISessionService sessionService
        )
        {
            _videoPlayer = videoPlayer;
            _backgroundMusicService = backgroundMusicService;
            _scenesService = scenesService;
            _videoBase = videoBase;
            _sessionService = sessionService;
        }

        public void Initialize()
        {
            _backgroundMusicService.Stop();

            var ending = _sessionService.WinEnding;
            var videoName = ending switch
            {
                EWinEnding.Defuse => _videoBase.DefuseWinVideoName,
                EWinEnding.Meal => _videoBase.MealWinVideoName,
                _ => throw new ArgumentOutOfRangeException()
            };
            _videoPlayer.Play(videoName);
            
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