using System;
using System.Collections.Generic;
using Alchemy.Inspector;
using Game.Utils.Dialog;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Game.Db.Dialog.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Dialogs + nameof(AvatarBase), fileName = nameof(AvatarBase))]
    public class AvatarBase : ScriptableObject, IAvatarBase
    {
        private const string SEARCH_FOLDER = "Assets/Config/Parameters/";
        
        [SerializeField] private List<NpcAvatarData> _npcAvatars;
        
        public Sprite GetAvatarByName(string avatarName)
        {
            foreach (var avatarData in _npcAvatars)
            {
                if (!avatarData.TryGetAvatarByName(avatarName, out var needSprite))
                    continue;

                return needSprite;
            }

            throw new Exception($"Avatar with name {avatarName} does not exist");
        }

#if UNITY_EDITOR
        [Button]
        public void AutoFill()
        {
            _npcAvatars = new List<NpcAvatarData>();
            var assets = AssetDatabase.FindAssets("t:NpcAvatarData", new[] {SEARCH_FOLDER});
            foreach (var asset in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                var data = AssetDatabase.LoadAssetAtPath<NpcAvatarData>(path);
                _npcAvatars.Add(data);
            }
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}