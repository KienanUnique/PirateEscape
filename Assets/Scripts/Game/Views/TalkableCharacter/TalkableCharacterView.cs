using DG.Tweening;
using Game.Core;
using Game.Db.Dialog.Impl;
using Game.Services.Dialog;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Views.TalkableCharacter
{
    public class TalkableCharacterView : AView, IInteractable
    {
        private const float SHOW_ALPHA_VALUE = 1f;
        private const float HIDE_ALPHA_VALUE = 0f; 
        
        private readonly ReactiveProperty<bool> _isInDialog = new();
        
        [Header("Appearance")]
        [SerializeField] private float _appearDuration = 0.5f;  
        [SerializeField] private Ease _appearEase = Ease.OutCubic;
        
        [Header("Disappearance")]
        [SerializeField] private float _disappearDuration = 0.2f;  
        [SerializeField] private Ease _disappearEase = Ease.InCubic;
        
        [SerializeField] private DialogProvider _dialog;
        [SerializeField] private Image _image;
        
        [Inject] private IDialogService _dialogService;
        
        private Tween _tween;
        
        public Vector3 Position => transform.position;

        protected override void OnInitialize()
        {
            _isInDialog.Subscribe(OnIsInDialog).AddTo(this);
            _dialogService.DialogComplete.Subscribe(_ => OnDialogComplete()).AddTo(this);
        }

        public void Interact()
        {
            _dialogService.StartDialog(_dialog);
            _isInDialog.Value = true;
        }

        private void OnDialogComplete()
        {
            _isInDialog.Value = false;
        }

        private void OnIsInDialog(bool isInDialog)
        {
            var targetAlpha = isInDialog ? HIDE_ALPHA_VALUE : SHOW_ALPHA_VALUE;
            var targetDuration = isInDialog ? _disappearDuration : _appearDuration;
            var targetEase = isInDialog ? _disappearEase : _appearEase;
            
            _tween?.Kill();
            _tween = _image.DOFade(targetAlpha, targetDuration).SetEase(targetEase).SetLink(_image.gameObject);
        }
    }
}