using System.Collections.ObjectModel;
using UnityEngine;

public class ArchimedesForce : MonoBehaviour {

    public WaterPart WaterLayer;
    
    private Collection<Rigidbody> _objectsInWater;
    private Collection<ObjectInWater> _topLayerMemory;
    private float _layerHeight;

    private class ObjectInWater {
        public readonly Rigidbody ObjectRigidbody;
        public int LayerPasses;
        public float FixedY;

        public ObjectInWater(Rigidbody objectRigidbody) {
            ObjectRigidbody = objectRigidbody;
            LayerPasses = 0;
            FixedY = 0;
        }
    }

    private void Start() {
        _layerHeight = gameObject.GetComponent<Transform>().position.y;
        _objectsInWater = new Collection<Rigidbody>();
        _topLayerMemory = new Collection<ObjectInWater>();
    }

    private void Update() {
        // If top layer passed enough times - block objects from moving (give y position of layer)
        if (WaterLayer.IsTopLayer) {
            foreach (var waterObject in _topLayerMemory) {
                var rigidbodyTransform = waterObject.ObjectRigidbody.GetComponent<Transform>();

                if (rigidbodyTransform.position.y > _layerHeight + WaterLayer.ParentWater.WaterLayerHeight * 4 ||
                    rigidbodyTransform.position.y < _layerHeight - WaterLayer.ParentWater.WaterLayerHeight * 4) {
                    waterObject.ObjectRigidbody.useGravity = true;
                    continue;
                }

                if (waterObject.LayerPasses < WaterLayer.ParentWater.TopLayerPassesBeforeBlock) {
                    waterObject.LayerPasses++;
                } else if (waterObject.LayerPasses == WaterLayer.ParentWater.TopLayerPassesBeforeBlock) {
                    waterObject.LayerPasses++;
                    waterObject.FixedY = rigidbodyTransform.position.y;
                } else {
                    waterObject.ObjectRigidbody.useGravity = false;
                    var position = rigidbodyTransform.position;
                    position = new Vector3(position.x, waterObject.FixedY, position.z);
                    rigidbodyTransform.position = position;
                }
            }
        }
        
        // Update all objects in water
        for (var index = 0; index < _objectsInWater.Count; index++) {
            var thisObject = _objectsInWater[index];
            
            // Sorry for this magic number, sometimes OnTriggerExit does not work
            var rigidbodyTransform = thisObject.GetComponent<Transform>();
            if (rigidbodyTransform.position.y > _layerHeight + WaterLayer.ParentWater.WaterLayerHeight * 6 ||
                rigidbodyTransform.position.y < _layerHeight - WaterLayer.ParentWater.WaterLayerHeight * 6) {
                _objectsInWater.Remove(thisObject);
                break;
            }

            // If not top player ignore boxes that are affected by top layer
            if (!thisObject.useGravity) continue;
            thisObject.AddForce(0, WaterLayer.ArchimedesForce * Time.deltaTime, 0);
        }
    }

    public void DisableBlocks() {
        if (_topLayerMemory == null) return;
        
        foreach (var waterObject in _topLayerMemory) {
            waterObject.ObjectRigidbody.useGravity = true;
        }
        _topLayerMemory.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != Constants.LAYER_FLOATING) return; // Ignore all except wooden boxes
        if (_objectsInWater == null) return; // Sometimes Start runs too late
        
        var objRigidbody = other.gameObject.GetComponent<Rigidbody>();
        _objectsInWater.Add(objRigidbody);
        if (WaterLayer.IsTopLayer) _topLayerMemory.Add(new ObjectInWater(objRigidbody));
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer != Constants.LAYER_FLOATING) return;
        other.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    
}