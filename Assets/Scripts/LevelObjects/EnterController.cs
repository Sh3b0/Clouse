using UnityEngine;

// Checks if player entered enter,
// then triggers the event if so (to show results screen or load new level)
public class EnterController : MonoBehaviour {

    public GameObject Tip;
    
    private bool _triggerEntered;

    private void Start() {
        _triggerEntered = false;
    }

    private void Update() {
        if (!_triggerEntered) return;
        if (Input.GetKeyDown("e")) {
            EventManager.TriggerEvent(Constants.EVENT_ENTER_REACHED);
        }
    }

    // Entered enter trigger
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        Tip.SetActive(true);
        _triggerEntered = true;
    }
    
    // Left enter trigger
    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        Tip.SetActive(false);
        _triggerEntered = false;
    }
    
}
