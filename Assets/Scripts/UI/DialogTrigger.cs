using UnityEngine;

public class DialogTrigger : MonoBehaviour {
    
    // TODO Think of better solution that will include dialog index
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        EventManager.TriggerEvent(Constants.EVENT_DIALOG);
        Destroy(gameObject);
    }
}
