using UnityEngine;

public class Snow : MonoBehaviour {
    
    // Whenever snow particle hits any collider, check if there is a water below
    // If so, freeze it
    private void OnParticleCollision(GameObject other) {
        if (other.layer != Constants.LAYER_FILLABLE) { return; }

        const int layerMask = 1 << Constants.LAYER_WATER; // Water layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit,
            Mathf.Infinity, layerMask)) {
            // Freeze the top layer of water on snowHit
            var waterPart = hit.collider.gameObject.GetComponent<WaterPart>();
            waterPart.ParentWater.FreezeLake();
        }
    }
    
}