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
        [SerializeField] private Collider _grabTrigger;
        [SerializeField] private Rigidbody _connector;

        [Inject] private IInputService _inputService;
        
        private bool _isGrab;
        private IGrabbable _grabObject;
        private IGrabbable _possibleGrabObject;

        public override void Initialize()
        {
            _grabTrigger.OnTriggerEnterAsObservable().Subscribe(OnGrabObjectAreaEnter).AddTo(this);
            _grabTrigger.OnTriggerExitAsObservable().Subscribe(OnGrabObjectAreaExit).AddTo(this);

            _inputService.GrabPerformed.Subscribe(_ => GrabActionHandle()).AddTo(this);
        }

        private void OnGrabObjectAreaEnter(Collider  other)
        {
            if (!other.IsOnLayer(LayerMasks.Grabbable))
                return;
            
            if (!other.TryGetComponent(out IGrabbable grabbable))
                return;
            
            _possibleGrabObject = grabbable;
        }
        
        private void OnGrabObjectAreaExit(Collider  other)
        {
            if (!other.IsOnLayer(LayerMasks.Grabbable))
                return;
            
            if (!other.TryGetComponent(out IGrabbable grabbable))
                return;
            
            if (_possibleGrabObject == grabbable)
            {
                _possibleGrabObject = null;
            }
        }
        
        private void GrabActionHandle()
        {
            if (_isGrab)
            {
                if (_grabObject != null)
                {
                    _grabObject.Drop();
                    _grabObject = null;
                }
                
                _isGrab = false;
            }
            else
            {
                if (_possibleGrabObject != null)
                {
                    _grabObject = _possibleGrabObject;
                    _grabObject.Grab(_connector);
                }
                
                _isGrab = true;
            }
        }
    }
}