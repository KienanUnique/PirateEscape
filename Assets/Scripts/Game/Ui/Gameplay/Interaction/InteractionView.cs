using DG.Tweening;
using KoboldUi.Element.View;
using UnityEngine;

namespace Game.Ui.Gameplay.Interaction
{
    public class InteractionView : AUiAnimatedView
    {
        private const float SHOW_ALPHA_VALUE = 1f;
        private const float HIDE_ALPHA_VALUE = 0f;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Appearance")]
        [SerializeField] private float _appearDuration = 0.5f;  
        [SerializeField] private Ease _appearEase = Ease.OutCubic;
        
        [Header("Disappearance")]
        [SerializeField] private float _disappearDuration = 0.3f;  
        [SerializeField] private Ease _disappearEase = Ease.InCubic;
        
        private Tween _currentTween;
        
        public void Appear()
        {
            _currentTween?.Kill();
            _currentTween = _canvasGroup.DOFade(SHOW_ALPHA_VALUE, _appearDuration).SetEase(_appearEase)
                .SetLink(_canvasGroup.gameObject);
        }

        public void Disappear()
        {
            _currentTween?.Kill();
            _currentTween = _canvasGroup.DOFade(HIDE_ALPHA_VALUE, _disappearDuration).SetEase(_disappearEase)
                .SetLink(_canvasGroup.gameObject);
        }

        public void HideInstantly()
        {
            _currentTween?.Kill();
            _canvasGroup.alpha = HIDE_ALPHA_VALUE;
        }
    }
}