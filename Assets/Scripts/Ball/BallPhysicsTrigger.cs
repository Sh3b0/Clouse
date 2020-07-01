using UnityEngine;

namespace Ball
{
    public sealed class BallPhysicsTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BallBase ball))
                ball.EnablePhysics();
        }
    }
}