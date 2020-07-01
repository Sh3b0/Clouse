using UnityEngine;

// Checks if player entered enter,
// then triggers the event if so (to show results screen or load new level)
public class EnterController : MonoBehaviour {

    // Entered enter trigger
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        if (LevelsManager.CurrentLevel == 1) {
            print("That is the first level");
            return;
        }
        LevelsManager.OnEnterReached();
    }
    
}
