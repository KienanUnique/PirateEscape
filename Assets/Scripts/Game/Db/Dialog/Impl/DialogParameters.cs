using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Game.Db.Dialog.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Dialogs + nameof(DialogParameters), fileName = nameof(DialogParameters))]
    public class DialogParameters : ScriptableObject, IDialogParameters
    {
        [SerializeField] private List<DialogWinEndingVo> _endingVos;
        
        [field: SerializeField] public string ChangeAvatarCommandName { get; private set; } = "change_avatar";
        [field: SerializeField] public string WinCommandName { get; private set; } = "win";
        [field: SerializeField] public string LoseCommandName { get; private set; } = "lose";

        public EWinEnding GetEndingByName(string winEndingName)
        {
            foreach (var dialogWinEndingVo in _endingVos)
            {
                if (!dialogWinEndingVo.EndingName.Equals(winEndingName))
                    continue;
                
                return dialogWinEndingVo.EndingType;
            }
            return EWinEnding.None;
        }

        [Serializable]
        private class DialogWinEndingVo
        {
            [field: SerializeField] public string EndingName { get; private set; }
            [field: SerializeField] public EWinEnding EndingType { get; private set; }
        }
    }
}