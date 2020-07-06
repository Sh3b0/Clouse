using UnityEngine;

public class Rain : MonoBehaviour {
    
    private void OnParticleCollision(GameObject other) {  
        if (other.layer != Constants.LAYER_FILLABLE) { return; }

        const int layerMask = 1 << Constants.LAYER_WATER; // Water layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity, layerMask)) {
            var waterPart = hit.collider.gameObject.GetComponent<WaterPart>();
            waterPart.ParentWater.IncreaseWaterlevel();
        }
    }
    
}