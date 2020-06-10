using UnityEngine;

public class Rain : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {  
        if (other.gameObject.CompareTag("Cloud")) { return; }

        const int layerMask = 1 << 4; // Water layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity, layerMask)) 
        {
            print("Rain hits the lake");
            var waterRise = hit.collider.gameObject.GetComponent<WaterRise>();
            waterRise.IncreaseWaterlevel();
        }
        else 
        {
            print("Rain hits a normal ground"); // Sound effect here
        }
        
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    }
}