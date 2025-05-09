using UnityEngine;

namespace Game.Db.Dialog
{
    public interface IAvatarBase
    {
        Sprite GetAvatarByName(string avatarName);
    }
}