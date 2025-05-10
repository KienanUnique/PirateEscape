using System.Collections.Generic;
using Game.Core;
using Game.Utils.Layers;
using R3;
using R3.Triggers;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Game.Views.Player.Interactor
{
    public class PlayerClickInteract : AModule
    {
        private readonly HashSet<IClickInteractable> _clickInteractables = new();
        private readonly ReactiveProperty<IClickInteractable> _chosenClickInteractable = new();
        private readonly ReactiveCommand _clickOnObjectInteractable = new();
        
        [SerializeField] private Collider _interactTrigger;

        [Inject] private IInputService _inputService;
        
        public ReadOnlyReactiveProperty<IClickInteractable> ChosenClickInteractable => _chosenClickInteractable;
        public Observable<Unit> ClickOnObjectInteractable => _clickOnObjectInteractable;

        public override void Initialize()
        {
            _interactTrigger.OnTriggerEnterAsObservable().Subscribe(OnGrabObjectAreaEnter).AddTo(this);
            _interactTrigger.OnTriggerExitAsObservable().Subscribe(OnGrabObjectAreaExit).AddTo(this);

            _inputService.ClickInteractPerformed.Subscribe(_ => TryClickInteractHandle()).AddTo(this);
        }

        private void OnGrabObjectAreaEnter(Collider  other)
        {
            if (!other.IsOnLayer(LayerMasks.Interactable))
                return;
            
            if (!other.TryGetComponent(out IClickInteractable clickInteractable))
                return;

            _clickInteractables.Add(clickInteractable);
            
            UpdateClosestClickInteractable();
        }
        
        private void OnGrabObjectAreaExit(Collider  other)
        {
            if (!other.IsOnLayer(LayerMasks.Interactable))
                return;
            
            if (!other.TryGetComponent(out IClickInteractable clickInteractable))
                return;
            
            _clickInteractables.Remove(clickInteractable);
            
            UpdateClosestClickInteractable();
        }

        private void TryClickInteractHandle()
        {
            if(_chosenClickInteractable.Value == null)
                return;
            
            _chosenClickInteractable.Value?.Interact();
            _clickOnObjectInteractable.Execute(Unit.Default);
        }

        private void UpdateClosestClickInteractable()
        {
            _chosenClickInteractable.Value = GetClosestClickInteractable();
        }
        
        private IClickInteractable GetClosestClickInteractable()
        {
            var thisPosition = transform.position;

            var closestDistance = float.PositiveInfinity;
            IClickInteractable closestInteractable = null;
            
            foreach (var interactable in _clickInteractables)
            {
                var distance = (interactable.Position - thisPosition).sqrMagnitude;
                if (distance > closestDistance)
                    continue;

                closestDistance = distance;
                closestInteractable = interactable;
            }

            return closestInteractable;
        }
    }
}