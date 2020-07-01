using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static bool snowHit;
    private bool _inTrigger;

    private void LateUpdate()
    {
        // if player is underground, don't fix Y.
        CameraController.fixY = !_inTrigger;
        if (snowHit && LevelsManager.CurrentLevel == 5)
        {
            GetComponent<BoxCollider>().isTrigger = false;
            snowHit = false;
        }
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