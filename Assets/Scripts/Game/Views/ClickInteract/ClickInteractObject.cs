using Game.Core;
using R3;
using UnityEngine;

namespace Game.Views.ClickInteract
{
    public class ClickInteractObject : AView, IClickInteractable
    {
        private readonly ReactiveCommand _finishInteractProgress = new();
        //_finishInteractProgress.Execute(transform(root).GetHashCode()) something like this logic for activate after complete clicks
        
        [SerializeField] private float _progressPerClick = 0.35f;

        public Observable<Unit> FinishInteractProgress => _finishInteractProgress;
        public Vector3 Position => transform.position;
        
        public float Progress { get; private set; }

        public void Interact()
        {
            if (Progress >= 1f)
                return;

            if (Progress + _progressPerClick >= 1)
            {
                Progress = 1f;
                
                _finishInteractProgress.Execute(Unit.Default);
                
                return;
            }
            
            Progress += _progressPerClick;
        }
    }
}