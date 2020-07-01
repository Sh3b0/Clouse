using System.Collections.ObjectModel;
using UnityEngine;

public class ArchimedesForce : MonoBehaviour {

    public WaterPart WaterLayer;
    
    private Collection<Rigidbody> _rigidBodiesInWater;
    private float _layerHeight;
    
    private void Start() {
        _layerHeight = gameObject.GetComponent<Transform>().position.y;
        _rigidBodiesInWater = new Collection<Rigidbody>();
    }

    private void Update() {
        foreach (var thisRigidBody in _rigidBodiesInWater) {
            thisRigidBody.AddForce(0, WaterLayer.ArchimedesForce, 0);
            var rigidbodyHeight = thisRigidBody.GetComponent<Transform>().position.y;
            
            if (rigidbodyHeight > _layerHeight + WaterLayer.ParentWater.WaterLayerHeight * 6 ||
                rigidbodyHeight < _layerHeight - WaterLayer.ParentWater.WaterLayerHeight * 6) {
                _rigidBodiesInWater.Remove(thisRigidBody);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Box")) return;
        _rigidBodiesInWater.Add(other.gameObject.GetComponent<Rigidbody>());
    }

    private void OnTriggerExit(Collider other) {
        if (!other.gameObject.CompareTag("Box")) return;
        other.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}