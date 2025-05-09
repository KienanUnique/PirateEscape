using FMOD.Studio;
using Services.FmodSound.Utils;

namespace Services.FmodSound
{
    public interface IGlobalSoundsService
    {
        void SetPause(ESoundsGroupType groupType, bool isPaused);
        void SetMute(ESoundsGroupType groupType, bool isMute);
        void Stop(ESoundsGroupType groupType, STOP_MODE stopMode = STOP_MODE.IMMEDIATE);
    }
}