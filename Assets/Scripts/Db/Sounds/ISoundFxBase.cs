using System.Collections.Generic;
using FMODUnity;
using Services.FmodSound.Utils;

namespace Db.Sounds
{
    public interface ISoundFxBase
    {
        IReadOnlyDictionary<EGameSoundFxType, EventReference> GameSoundsEventReferences { get; }
        IReadOnlyDictionary<EUiSoundFxType, EventReference> UiSoundsEventReferences { get; }
        IReadOnlyDictionary<EBackgroundMusicType, string> BackgroundMusicTypeParametersNames { get; }
        string BackgroundMusicTypeParameterName { get; }
        EventReference BackgroundMusicEventReference { get; }
        IReadOnlyDictionary<ESoundsGroupType, string> SoundGroupBuses { get; }
    }
}