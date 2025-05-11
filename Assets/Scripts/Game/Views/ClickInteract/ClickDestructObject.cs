using DG.Tweening;
using UnityEngine;

namespace Game.Views.ClickInteract
{
    public class ClickDestructObject : MonoBehaviour, IClickInteractable
    {
        [SerializeField] private int _progressPerClick = 1;
        [field: SerializeField] public int MaxProgress { get; private set; } = 10;
        
        [SerializeField] private Transform _containerToScale;
        [SerializeField] private Collider _collider;
        
        [Header("Punch Animation")]
        [SerializeField] private float _punchedScale = 0.7f;
        [SerializeField] private float _punchDuration = 0.3f;
        [SerializeField] private Ease _punchEase = Ease.Linear;
        
        [Header("Destroy Animation")]
        [SerializeField] private ParticleSystem _destroyParticles;
        [SerializeField] private float _destroyDuration = 0.15f;
        [SerializeField] private Ease _destroyEase = Ease.InBack;
        
        public bool CanInteract { get; private set; } = true;
        
        private Tween _punchTween;
        
        public Vector3 Position => transform.position;
        public int Progress { get; private set; }

        public void Interact()
        {
            if (Progress >= MaxProgress)
                return;

            if (Progress + _progressPerClick >= MaxProgress)
            {
                Progress = MaxProgress;
                CanInteract = false;
                AnimateDestruct();
                return;
            }
            
            Progress += _progressPerClick;
            AnimatePunch();
        }

        private void Awake()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_containerToScale.DOScale(_punchedScale, _punchDuration / 2));
            sequence.Append(_containerToScale.DOScale(1f, _punchDuration / 2));
            sequence.SetEase(_punchEase);
            sequence.SetLink(_containerToScale.gameObject);
            sequence.SetAutoKill(false);
            sequence.Pause();
            
            _punchTween = sequence;
        }

        private void AnimatePunch()
        {
            _punchTween?.Restart();
        }

        private void AnimateDestruct()
        {
            _destroyParticles.Play();
            _punchTween?.Kill();
            _containerToScale.DOScale(0f, _destroyDuration).SetEase(_destroyEase).SetLink(_containerToScale.gameObject)
                .OnComplete(() => _collider.enabled = false);
        }
    }
}