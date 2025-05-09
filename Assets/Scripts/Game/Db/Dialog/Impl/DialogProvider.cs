using UnityEngine;
using Utils;

namespace Game.Db.Dialog.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Dialogs + nameof(DialogProvider), fileName = nameof(DialogProvider))]
    public class DialogProvider : ScriptableObject, IDialogProvider
    {
        [SerializeField] private string _startNode;

        public string StartNode => _startNode;
    }
}