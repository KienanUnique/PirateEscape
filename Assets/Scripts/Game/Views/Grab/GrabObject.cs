using Game.Core;
using UnityEngine;

namespace Game.Views.Grab
{
    public class GrabObject : AView, IGrabbable
    {
        public Vector3 Position => transform.position;

        public void Grab(Rigidbody connect)
        {
            
        }

        public void Drop()
        {
            
        }
    }
}