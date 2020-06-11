using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Target, Boy, Cloud;
    
    private Vector3 _offset;

    private void Start() {
        Target = Boy;
        _offset = Target.position - transform.position;
    }

    private void Update() {
        if (Player.playerActive) Target = Boy;
        else Target = Cloud;
        transform.position = Target.position - _offset;
    }
    
}
