using UnityEngine;

public sealed class PassTarget : MonoBehaviour
{
    public Vector3 Position => transform.position;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Position, 0.25f);
    }
}