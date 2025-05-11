using System;
using System.Collections.Generic;
using Alchemy.Inspector;
using Alchemy.Serialization;
using FMODUnity;
using Services.FmodSound.Utils;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Db.Sounds.Impl
{
    [AlchemySerialize]
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(SoundFxBase), fileName = nameof(SoundFxBase))]
    public partial class SoundFxBase : ScriptableObject, ISoundFxBase
    {
        [SerializeField] private List<SoundClipVo> gameSounds = new();
        [SerializeField] private List<SoundClipVo> uiSounds = new();

        [AlchemySerializeField, NonSerialized]
        private Dictionary<ESoundsGroupType, string> _soundGroupBuses = new();
        
        [field: SerializeField] public EventReference BackgroundMusicEventReference { get; private set; }

        public IReadOnlyDictionary<ESoundsGroupType, string> SoundGroupBuses => _soundGroupBuses;

        public IReadOnlyDictionary<EGameSoundFxType, EventReference> GameSoundsEventReferences
        {
            get
            {
                var result = new Dictionary<EGameSoundFxType, EventReference>();
                foreach (var soundClipVo in gameSounds)
                    result.Add(soundClipVo.GameSoundType, soundClipVo.EventReference);
                
                return result;
            }
        }

        public IReadOnlyDictionary<EUiSoundFxType, EventReference> UiSoundsEventReferences
        {
            get
            {
                var result = new Dictionary<EUiSoundFxType, EventReference>();
                foreach (var soundClipVo in uiSounds)
                    result.Add(soundClipVo.UiSoundName, soundClipVo.EventReference);
                
                return result;
            }
        }

#if UNITY_EDITOR
        [Button]
        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
#endif

        [Serializable]
        private class SoundClipVo
        {
            public EUiSoundFxType UiSoundName;
            public EGameSoundFxType GameSoundType;
            public EventReference EventReference;
        }
    }
}