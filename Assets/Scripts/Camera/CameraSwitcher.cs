using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Vector3 offset;
    private bool _switched, _inTrigger;
    private void Update(){
        if (Player.playerActive)
        {
            if (_inTrigger && !_switched)
            {
                _switched = true;
                CameraController.me.transform.position += offset;   
            }
            else if (!_inTrigger && _switched)
            {
                _switched = false;
                CameraController.me.transform.position -= offset;
            }
        }
        else if (Player.cloudActive)
        {
            if (_inTrigger && _switched)
            {
                _switched = false;
                CameraController.me.transform.position -= offset;   
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inTrigger = true;
            _switched = true;
            CameraController.me.transform.position += offset;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inTrigger = false;
            _switched = false;
            CameraController.me.transform.position -= offset;
        }
    }
    public static void ResetCamera()
    {
        Vector3 newPos = Camera.main.transform.position;
        newPos.y = 0;
        Camera.main.transform.position = newPos;
    }
}
