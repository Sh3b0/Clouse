using System;
using UnityEngine;
using Validation;

namespace Ball
{
    [RequireComponent(typeof(BallBase))]
    public sealed class BallMovement : MonoBehaviour
    {
        public BallBase Ball => _ball;
        public CurveMovement Curve => _curve;

        private void OnEnable()
        {
            _ball.PhysicsEnabled += _onPhysicsEnabled;
        }

        private void OnDisable()
        {
            _ball.PhysicsEnabled -= _onPhysicsEnabled;
        }

        private void Awake()
        {
            gameObject.RequireInChildren(out _curve);
            gameObject.Require(out _ball);

            _curve.Target = _ball.transform;
            _onPhysicsEnabled = (sender, args) =>
            {
                var velocity = _curve.GetCurrentVelocity();
                _curve.ForceStop();
                _ball.Rigidbody.AddForce(velocity, ForceMode.VelocityChange);
            };
        }

        private EventHandler _onPhysicsEnabled;
        private BallBase _ball;
        private CurveMovement _curve;
    }
}