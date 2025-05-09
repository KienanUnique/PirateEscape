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
    public class PlayerInteractor : AModule
    {
        private readonly HashSet<IInteractable> _interactables = new();
        private readonly ReactiveProperty<bool> _canInteract = new();

        [SerializeField] private Collider _interactionTrigger;

        [Inject] private IInputService _inputService;

        private bool _isInteractionEnabled;

        public override void Initialize()
        {
            _interactionTrigger.OnTriggerEnterAsObservable().Subscribe(OnInteractionTriggerEnter).AddTo(this);
            _interactionTrigger.OnTriggerExitAsObservable().Subscribe(OnInteractionTriggerExit).AddTo(this);

            _inputService.InteractionPerformed.Subscribe(_ => TryInteract()).AddTo(this);
        }

        private void TryInteract()
        {
            if (!_canInteract.Value)
                return;

            var closestInteractable = GetClosestInteractable();
            closestInteractable.Interact();
        }

        public void EnableInteraction()
        {
            _isInteractionEnabled = true;
            UpdateInteractionStatus();
        }

        public void DisableInteraction()
        {
            _isInteractionEnabled = false;
            UpdateInteractionStatus();
        }

        private void OnInteractionTriggerEnter(Collider other)
        {
            if (!other.IsOnLayer(LayerMasks.Interactable))
                return;

            if (!other.TryGetComponent(out IInteractable interactable))
                return;

            _interactables.Add(interactable);
            UpdateInteractionStatus();
        }

        private void OnInteractionTriggerExit(Collider other)
        {
            if (!other.IsOnLayer(LayerMasks.Interactable))
                return;

            if (!other.TryGetComponent(out IInteractable interactable))
                return;

            if (!_interactables.Contains(interactable))
                return;

            _interactables.Remove(interactable);
            UpdateInteractionStatus();
        }

        private void UpdateInteractionStatus()
        {
            if (!_isInteractionEnabled)
            {
                _canInteract.Value = false;
                return;
            }

            _canInteract.Value = _interactables.Count > 0;
        }

        private IInteractable GetClosestInteractable()
        {
            var thisPosition = transform.position;

            var closestDistance = float.PositiveInfinity;
            IInteractable closestInteractable = null;
            foreach (var interactable in _interactables)
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