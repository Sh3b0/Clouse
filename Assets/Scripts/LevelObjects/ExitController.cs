using UnityEngine;

// Checks if player entered exit,
// then triggers the event if so (to show results screen or load new level)
public class ExitController : MonoBehaviour {

    // Entered exit trigger
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        if (LevelsManager.CurrentLevelIsLast()) {
            print("That is the last level");
            return;
        }
        LevelsManager.OnExitReached();
    }
    
}
