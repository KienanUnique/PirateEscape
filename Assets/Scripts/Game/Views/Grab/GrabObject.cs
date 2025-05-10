using Game.Core;
using UnityEngine;

namespace Game.Views.Grab
{
    public class GrabObject : AView, IGrabbable
    {
        private SpringJoint _joint;
        
        public Vector3 Position => transform.position;

        public void Grab(Rigidbody connector)
        {
            _joint = gameObject.AddComponent<SpringJoint>();
            _joint.connectedBody = connector;
            _joint.spring = 150f;
            _joint.damper = 15f;
            //_joint.autoConfigureConnectedAnchor = false;
            //_joint.anchor = Vector3.zero;
            //_joint.connectedAnchor = Vector3.zero;
        }

        public void Drop()
        {
            Destroy(_joint);
        }
    }
}