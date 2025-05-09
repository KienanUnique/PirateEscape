using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Game.Utils.Dialog
{
    [CreateAssetMenu(menuName = MenuPathBase.Dialogs + nameof(NpcAvatarData), fileName = nameof(NpcAvatarData))]
    public class NpcAvatarData : ScriptableObject
    {
        [SerializeField] private List<ConcreteAvatarData> _avatars;

        public bool TryGetAvatarByName(string avatarName, out Sprite avatarSprite)
        {
            foreach (var avatar in _avatars)
            {
                if (!avatar.Name.Equals(avatarName))
                {
                    continue;
                }

                avatarSprite = avatar.Sprite;
                return true;
            }

            avatarSprite = null;
            return false;
        }
        
        [Serializable]
        private struct ConcreteAvatarData
        {
            [SerializeField] private string _name;
            [SerializeField] private Sprite _sprite;

            public Sprite Sprite => _sprite;
            public string Name => _name;
        }
    }
}