using UnityEngine;

public class Box : MonoBehaviour
{
    public float speed;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Player.playerActive)
        {
            print("Now holding the box"); // Some animation of the player holding the box
            GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0));
            // transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal"), transform.position.y, transform.position.z);
        }
    }
}
