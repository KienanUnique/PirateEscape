using Game.Core;
using Game.Utils.Layers;
using R3;
using R3.Triggers;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Game.Views.Player.Interactor
{
    public class PlayerGrab : AModule
    {
        [SerializeField] private Collider _grabTrigger;
        [SerializeField] private Rigidbody _connector;

        [Inject] private IInputService _inputService;
        
        private bool _isGrab;
        private IGrabbable _grabObject;
        private IGrabbable _possibleGrabObject;
        private readonly ReactiveProperty<bool> _canGrab = new();

        public ReadOnlyReactiveProperty<bool> CanGrab => _canGrab;

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

            if (!_isGrab)
            {
                _canGrab.Value = true;
            }
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
                _canGrab.Value = false;
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
                if (_possibleGrabObject == null) 
                    return;
                
                _grabObject = _possibleGrabObject;
                _grabObject.Grab(_connector);
                _isGrab = true;
                _canGrab.Value = false;
            }
        }
    }
}