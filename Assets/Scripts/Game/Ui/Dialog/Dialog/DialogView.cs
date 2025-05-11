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
        
        [Header("Sprite change")]
        [SerializeField] private float _spriteChangeSmallestScale = 0.7f;
        [SerializeField] private float _spriteChangeDuration = 0.3f;  
        [SerializeField] private Ease _spriteChangeEase = Ease.OutCubic;

        private Tween _currentTween;
        
        [field: SerializeField] public DialogueRunner Runner { get; private set; }
        [field: SerializeField] public LineView Line { get; private set; }

        public void ChangeAvatar(Sprite newAvatar, bool needHidePreviousAvatar)
        {
            _currentTween?.Kill(true);

            if (!needHidePreviousAvatar)
            {
                _currentTween = _image.DOFade(SHOW_ALPHA_VALUE, _appearDuration).SetEase(_appearEase)
                    .SetLink(_image.gameObject);

                return;
            }
            
            var sequence = DOTween.Sequence();
            var scaleDuration = _spriteChangeDuration / 2;
            sequence.Append(_image.rectTransform.DOScale(_spriteChangeSmallestScale, scaleDuration));
            sequence.AppendCallback(() => _image.sprite = newAvatar);
            sequence.Append(_image.rectTransform.DOScale(1f, scaleDuration));
            sequence.SetEase(_spriteChangeEase);
            sequence.SetLink(_image.gameObject);
            
            _currentTween = sequence;
        }

        public void HideAvatarInstantly()
        {
            var color = _image.color;
            color.a = HIDE_ALPHA_VALUE;
            _image.color = color;
        }
    }
}