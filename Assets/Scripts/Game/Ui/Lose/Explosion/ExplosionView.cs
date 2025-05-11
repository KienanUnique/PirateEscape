using DG.Tweening;
using KoboldUi.Element.View;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Lose.Explosion
{
    public class ExplosionView : AUiSimpleView
    {
        private const float SHOW_ALPHA_VALUE = 1f;
        private const float HIDE_ALPHA_VALUE = 0f;
        
        [SerializeField] private Image _whiteImage;
        [SerializeField] private Image _backgroundImage;
        
        [Header("Animation")] 
        [SerializeField] private float _appearDuration = 0.05f;
        [SerializeField] private Ease _appearEase = Ease.OutExpo;
        
        [SerializeField] private float _whiteScreenDuration = 0.5f;
        
        [SerializeField] private float _whiteScreenDisappearDuration = 0.5f;
        [SerializeField] private Ease _whiteScreenDisappearEase = Ease.InQuad;

        private void Awake()
        {
            ChangeAlpha(_whiteImage, HIDE_ALPHA_VALUE);
            ChangeAlpha(_backgroundImage, HIDE_ALPHA_VALUE);
        }

        public void Play()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_whiteImage.DOFade(SHOW_ALPHA_VALUE, _appearDuration).SetEase(_appearEase));
            sequence.AppendCallback(() => ChangeAlpha(_backgroundImage, SHOW_ALPHA_VALUE));
            sequence.AppendInterval(_whiteScreenDuration);
            sequence.Append(_whiteImage.DOFade(HIDE_ALPHA_VALUE, _whiteScreenDisappearDuration).SetEase(_whiteScreenDisappearEase));
            sequence.SetLink(_whiteImage.gameObject);
        }

        private void ChangeAlpha(Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}