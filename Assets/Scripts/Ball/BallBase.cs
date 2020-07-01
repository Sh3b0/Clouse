using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Validation;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class BallBase : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        public bool CanBePassed { get; private set; } = true;
        public Rigidbody Rigidbody => _rigidbody;

        public void EnablePhysics()
        {
            CanBePassed = false;
            _rigidbody.isKinematic = false;
            
            PhysicsEnabled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler PhysicsEnabled;

        private void Awake()
        {
            gameObject.Require(out _rigidbody);
        }

        private Rigidbody _rigidbody;
    }
}