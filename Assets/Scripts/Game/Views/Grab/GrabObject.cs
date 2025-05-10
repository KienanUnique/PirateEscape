using Game.Core;
using UnityEngine;

namespace Game.Views.Grab
{
    public class GrabObject : AView, IGrabbable
    {
        [SerializeField] private Rigidbody _selfRigidbody;
        
        private SpringJoint _joint;

        public void Grab(Rigidbody connector)
        {
            _joint = gameObject.AddComponent<SpringJoint>();
            _joint.connectedBody = connector;
            _joint.spring = 150f;
            _joint.damper = 15f;

            ChangeRigidbodyParams(false);
        }

        public void Drop()
        {
            Destroy(_joint);
            
            ChangeRigidbodyParams(true);
        }

        private void ChangeRigidbodyParams(bool toDefault)
        {
            if (toDefault)
            {
                _selfRigidbody.mass = 15;
                _selfRigidbody.linearDamping = 0;
                _selfRigidbody.angularDamping = 0.05f;
            }
            else
            {
                _selfRigidbody.mass = 5;
                _selfRigidbody.linearDamping = 2;
                _selfRigidbody.angularDamping = 2;
            }
        }
    }
}