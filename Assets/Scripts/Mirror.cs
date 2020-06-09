using UnityEngine;

public class Mirror : MonoBehaviour
{
    private bool moved;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("press E to interact"); // Visualize this
            if (!moved && Input.GetButton("Interact"))
            {
                GetComponent<Animation>().Play();
                moved = true;
            }
        }
    }
}
