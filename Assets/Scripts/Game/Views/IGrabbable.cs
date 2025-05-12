using UnityEngine;

namespace Game.Views
{
    public interface IGrabbable
    {
        void Grab(Rigidbody connector);
        void Drop();
    }
}