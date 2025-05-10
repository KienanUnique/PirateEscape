using Game.Core;
using Game.Services.CameraHolder;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Views.TalkableCharacter
{
    public class SpriteRotatorModule : AModule
    {
        [SerializeField] private Transform _objectToRotate;
        
        [Inject] private ICameraHolderService _cameraHolderService;

        public override void Initialize()
        {
            Observable.EveryUpdate(UnityFrameProvider.Update).Subscribe(_ => OnUpdate()).AddTo(this);
        }

        private void OnUpdate()
        {
            var cameraPosition = _cameraHolderService.CameraPosition;
            var toCamera = cameraPosition - _objectToRotate.position;
            
            var targetRotation = Quaternion.LookRotation(-toCamera);
            
            var euler = targetRotation.eulerAngles;
            var selfRotation = _objectToRotate.rotation;
            euler.x = selfRotation.eulerAngles.x;
            euler.z = transform.rotation.eulerAngles.z;
        
            _objectToRotate.rotation = Quaternion.Euler(euler);
        }
    }
}