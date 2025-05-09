using DG.Tweening;
using KoboldUi.Element.View;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Game.Ui.Dialog.Dialog
{
    public class DialogView : AUiAnimatedView
    {
        private const float SHOW_ALPHA_VALUE = 1f;
        private const float HIDE_ALPHA_VALUE = 0f; 
        
        [SerializeField] private Image _image;
        
        [Header("Appearance")]
        [SerializeField] private float _appearDuration = 0.5f;  
        [SerializeField] private Ease _appearEase = Ease.OutCubic;
        
        [Header("Disappearance")]
        [SerializeField] private float _disappearDuration = 0.3f;  
        [SerializeField] private Ease _disappearEase = Ease.InCubic;

        private Sequence _currentSequence;
        
        [field: SerializeField] public DialogueRunner Runner { get; private set; }

        public void ChangeAvatar(Sprite newAvatar, bool needHidePreviousAvatar)
        {
            _currentSequence?.Kill();
            
            _currentSequence = DOTween.Sequence();
            if (needHidePreviousAvatar)
                _currentSequence.Append(Disappear());

            _currentSequence.AppendCallback(() => _image.sprite = newAvatar);
            _currentSequence.Append(Appear());
            _currentSequence.SetLink(_image.gameObject);
        }

        private Tween Appear()
        {
            return _image.DOFade(SHOW_ALPHA_VALUE, _appearDuration).SetEase(_appearEase);
        }

        private Tween Disappear()
        {
            return _image.DOFade(HIDE_ALPHA_VALUE, _disappearDuration).SetEase(_disappearEase);
        }
    }
}