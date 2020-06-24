using UnityEngine;

// Checks if player entered exit,
// then triggers the event if so (to show results screen or load new level)
public class ExitController : MonoBehaviour {

    public GameObject Tip;
    
    private bool _triggerEntered;

    private void Start() {
        _triggerEntered = false;
    }

    private void Update() {
        if (!_triggerEntered) return;
        if (Input.GetKeyDown("e")) {
            EventManager.TriggerEvent(Constants.EVENT_EXIT_REACHED);
        }
    }

    // Entered exit trigger
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        Tip.SetActive(true);
        _triggerEntered = true;
    }
    
    // Left exit trigger
    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        Tip.SetActive(false);
        _triggerEntered = false;
    }
    
}
