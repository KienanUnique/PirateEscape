using Game.Core;
using R3;
using UnityEngine;

namespace Game.Views.ClickInteract
{
    public class ClickInteractObject : AView, IClickInteractable
    {
        private readonly ReactiveCommand _finishInteractProgress = new();
        //_finishInteractProgress.Execute(transform(root).GetHashCode()) something like this logic for activate after complete clicks
        
        [SerializeField] private int _progressPerClick = 1;
        [field: SerializeField] public int MaxProgress { get; private set; } = 10;

        public Observable<Unit> FinishInteractProgress => _finishInteractProgress;
        public Vector3 Position => transform.position;
        public int Progress { get; private set; }

        public void Interact()
        {
            if (Progress >= MaxProgress)
                return;

            if (Progress + _progressPerClick >= MaxProgress)
            {
                Progress = MaxProgress;
                
                _finishInteractProgress.Execute(Unit.Default);
                
                return;
            }
            
            Progress += _progressPerClick;
        }
    }
}