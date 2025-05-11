using R3;

namespace FinalTitles.Utils.Player
{
    public interface IFinalTitlesVideoPlayer
    { 
        Observable<Unit> VideoEnded { get; }
        void Play();
    }
}