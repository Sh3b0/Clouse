using UnityEngine;

public class Rain : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {  
        if (other.gameObject.CompareTag("Lake"))
        {
            print("Rain hits the lake");
            // Fill the water gap
        }
        else if (!other.gameObject.CompareTag("Cloud"))
        {
            print("Rain hits a normal ground"); // Sound effect here
        }
    }
}