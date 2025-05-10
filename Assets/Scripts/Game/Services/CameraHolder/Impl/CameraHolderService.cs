using UnityEngine;

namespace Game.Services.CameraHolder.Impl
{
    public class CameraHolderService : ICameraHolderService
    {
        private readonly Camera _camera;

        public Vector3 CameraPosition => _camera.transform.position;

        public CameraHolderService(Camera camera)
        {
            _camera = camera;
        }
    }
}