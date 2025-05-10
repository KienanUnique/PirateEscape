using UnityEngine;

namespace Game.Services.CameraHolder
{
    public interface ICameraHolderService
    {
        Vector3 CameraPosition { get; }
    }
}