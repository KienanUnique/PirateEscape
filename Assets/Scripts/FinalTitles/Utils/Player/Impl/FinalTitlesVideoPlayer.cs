using R3;
using Services.Settings;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace FinalTitles.Utils.Player.Impl
{
    public class FinalTitlesVideoPlayer : MonoBehaviour, IFinalTitlesVideoPlayer
    {
        private readonly ReactiveCommand _videoEnded = new();

        [Range(0f, 1f)] [SerializeField] private float _startVideoVolume = 0.5f;
        [SerializeField] private VideoPlayer _videoPlayer;

        [Inject] private ISettingsStorageService _settingsStorage;

        public Observable<Unit> VideoEnded => _videoEnded;

        public void Play(string videoName)
        {
            _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);

            var needVolume = _settingsStorage.MasterVolume.CurrentValue * _startVideoVolume;
            for (ushort i = 0; i < _videoPlayer.controlledAudioTrackCount; i++)
                _videoPlayer.SetDirectAudioVolume(i, needVolume);

            _videoPlayer.Play();
        }

        private void OnEnable()
        {
            _videoPlayer.loopPointReached += OnVideoEnded;
        }

        private void OnDisable()
        {
            _videoPlayer.loopPointReached -= OnVideoEnded;
        }
        
        private void OnVideoEnded(VideoPlayer videoPlayer)
        {
            _videoEnded.Execute(Unit.Default);
        }
    }
}