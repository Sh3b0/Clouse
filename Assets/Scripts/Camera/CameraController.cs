using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Target;
    private Vector3 _offset;

    private void Start() {
        _offset = Target.position - transform.position;
    }

    private void Update() {
        transform.position = new Vector3((Target.position - _offset).x, transform.position.y, transform.position.z);
    } 
}
