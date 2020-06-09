using UnityEngine;

public class CloudController : MonoBehaviour
{
    public float moveSpeed;
    void FixedUpdate()
    {
        if (Controller2D.cloudActive)
        {
            transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * moveSpeed, transform.position.y);
        }
    }
}
