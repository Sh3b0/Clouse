using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject KeyIcon;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Player.playerActive && Mathf.Abs(transform.rotation.z) <= 0.01f)
        {
            // Play animation of the player holding the box if not currently playing.
            if (Input.GetButton("Interact")) // If player currently holding the box
            {
                KeyIcon.SetActive(false);
                transform.parent = Player.me;
                Player.jumpingEnabled = false;
            }
            else // If player left the box
            {
                // stop the box holding animation.
                KeyIcon.SetActive(true);
                transform.parent = null;
                Player.jumpingEnabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Mathf.Abs(transform.rotation.z) <= 0.01)
            KeyIcon.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        // Stop any box holding animation
        KeyIcon.SetActive(false);
        transform.parent = null;
        Player.jumpingEnabled = true;
    }
}
