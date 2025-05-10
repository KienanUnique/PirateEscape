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
    public class PlayerGrabInteractor : AModule
    {
        private readonly HashSet<IGrabbable> _grabbables = new();
        
        [SerializeField] private Collider _grabTrigger;
        [SerializeField] private Rigidbody _connector;

        [Inject] private IInputService _inputService;
        
        private bool _isCanGrab;

        public override void Initialize()
        {
            _grabTrigger.OnTriggerEnterAsObservable().Subscribe(OnGrabObjectAreaEnter).AddTo(this);
            _grabTrigger.OnTriggerExitAsObservable().Subscribe(OnGrabObjectAreaExit).AddTo(this);

            _inputService.GrabPerformed.Subscribe(_ => TryGrab()).AddTo(this);
        }

        private void OnGrabObjectAreaEnter(Collider  other)
        {
            if (!other.IsOnLayer(LayerMasks.Grabbable))
                return;

            if (!other.TryGetComponent(out IGrabbable grabbable))
                return;
            
            _grabbables.Add(grabbable);
        }
        
        private void OnGrabObjectAreaExit(Collider  other)
        {
            if (!other.IsOnLayer(LayerMasks.Grabbable))
                return;

            if (!other.TryGetComponent(out IGrabbable grabbable))
                return;

            _grabbables.Remove(grabbable);
        }
        
        private void TryGrab()
        {
            if (!_isCanGrab)
                return;

            GrabObject();
        }

        private void GrabObject()
        {
            var isOneObject = _grabbables.Count == 1;
            
            foreach (var grabbable in _grabbables)
            {
                if (isOneObject)
                {
                    grabbable.Grab(_connector);
                    
                    break;
                }
                else
                {
                    //raycast
                }
            }
        }
    }
}