using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private bool _inTrigger;

    private void LateUpdate()
    {
        // if player is underground, don't fix Y.
        CameraController.fixY = !_inTrigger;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            _inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _inTrigger = false;
    }
}