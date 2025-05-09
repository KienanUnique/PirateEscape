using Game.Core;
using Game.Db.Dialog.Impl;
using Game.Services.Dialog;
using UnityEngine;
using Zenject;

namespace Game.Views.TalkableCharacter
{
    public class TalkableCharacterView : AView, IInteractable
    {
        [SerializeField] private DialogProvider _dialog;
        
        [Inject] private IDialogService _dialogService;
        
        public Vector3 Position => transform.position;
        
        public void Interact() => _dialogService.StartDialog(_dialog);
    }
}