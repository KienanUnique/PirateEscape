using UnityEngine;
using Utils;

namespace Game.Db.Dialog.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Dialogs + nameof(DialogParameters), fileName = nameof(DialogParameters))]
    public class DialogParameters : ScriptableObject, IDialogParameters
    {
        [field: SerializeField] public string ChangeAvatarCommandName { get; private set; } = "change_avatar";
    }
}