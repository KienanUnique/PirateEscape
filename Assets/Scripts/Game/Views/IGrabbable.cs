using UnityEngine;

namespace Game.Views
{
    public interface IGrabbable
    {
        Vector3 Position { get; }
        
        void Grab(Rigidbody connect);
        void Drop();
    }
}