using System.Collections.ObjectModel;
using UnityEngine;
public class ArchimedesForce : MonoBehaviour
{
    public float force;
    private Collection<Rigidbody> _rigidBodiesInWater;

    private void Start()
    {
        _rigidBodiesInWater = new Collection<Rigidbody>();
    }

    private void Update()
    {
        foreach (var thisRigidBody in _rigidBodiesInWater)
        {
            thisRigidBody.AddForce(0, force, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Box")) return;
        // print("Box entered");
        _rigidBodiesInWater.Add(other.gameObject.GetComponent<Rigidbody>());
        
        // Can't move box while under water, disable it's holding trigger and detach it from player.
        Box.LeaveBox(other.GetComponent<Box>());
        other.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Box")) return;
        _rigidBodiesInWater.Remove(other.gameObject.GetComponent<Rigidbody>());
        other.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}