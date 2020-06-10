using System.Collections.ObjectModel;
using UnityEngine;

public class ArchimedesForce : MonoBehaviour {

    public float Force;

    private Collection<Rigidbody> _rigidbodiesInWater;

    private void Start() {
        _rigidbodiesInWater = new Collection<Rigidbody>();
    }

    private void Update() {
        foreach (var thisRigidbody in _rigidbodiesInWater) {
            thisRigidbody.AddForce(0, Force, 0);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Floating")) {
            print("Box entered");
            _rigidbodiesInWater.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Floating")) {
            _rigidbodiesInWater.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
