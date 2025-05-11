using System;
using UnityEngine;

namespace Game.Views.Grab
{
    public class GrabObject : MonoBehaviour, IGrabbable
    {
        [SerializeField] private Rigidbody _selfRigidbody;
        [SerializeField] private RigidbodyParams _defaultParam;
        [SerializeField] private RigidbodyParams _grabParams;
        [SerializeField] private float _jointSpring = 150f;
        [SerializeField] private float _jointDamper = 15f;
        
        private SpringJoint _joint;

        public void Grab(Rigidbody connector)
        {
            _joint = gameObject.AddComponent<SpringJoint>();
            _joint.connectedBody = connector;
            _joint.spring = _jointSpring;
            _joint.damper = _jointDamper;

            SetRigidbodyParams(_grabParams);
        }

        public void Drop()
        {
            Destroy(_joint);
            
            SetRigidbodyParams(_defaultParam);
        }

        private void SetRigidbodyParams(RigidbodyParams rigidbodyParams)
        {
            _selfRigidbody.mass = rigidbodyParams.Mass;
            _selfRigidbody.linearDamping = rigidbodyParams.LinearDamping;
            _selfRigidbody.angularDamping = rigidbodyParams.AngularDamping;
        }
        
        [Serializable]
        private class RigidbodyParams
        {
            public float Mass;
            public float LinearDamping;
            public float AngularDamping;
        }
    }
}