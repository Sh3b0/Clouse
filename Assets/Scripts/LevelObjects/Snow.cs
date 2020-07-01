using UnityEngine;

public class Snow : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Cloud"))
        {
            return;
        }

        const int layerMask = 1 << 4; // Water layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit,
            Mathf.Infinity, layerMask))
        {
            //print("Snow hits the lake"); // TODO for peter: freeze the top layer of water on snowHit.    
            CameraSwitcher.snowHit = true;
            //var waterPart = hit.collider.gameObject.GetComponent<WaterPart>();
            //waterPart.ParentWater.IncreaseWaterlevel();
        }

        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    }
}