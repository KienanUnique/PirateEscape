using System.Collections.Generic;
using Alchemy.Inspector;
using Game.Core;
using Game.Views.ClickInteract;
using Game.Views.TalkableCharacter;
using UnityEditor;
using UnityEngine;

namespace Game.Utils.NonBindedViews
{
    public class NonBindedViewsHolder : MonoBehaviour
    {
        [SerializeField] private List<TalkableCharacterView> _talkableCharacters;
        
        public IReadOnlyList<AView> NonBindedViews
        {
            get
            {
                var views = new List<AView>();
                views.AddRange(_talkableCharacters);
                return views;
            }
        }

#if UNITY_EDITOR
        [Button]
        public virtual void Autofill()
        {
            _talkableCharacters = new List<TalkableCharacterView>();
            _talkableCharacters.AddRange(FindObjectsByType<TalkableCharacterView>(FindObjectsSortMode.InstanceID));
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}