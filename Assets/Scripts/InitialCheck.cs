using UnityEngine;

// The 'Pre-load script'
// Should be placed on first scene and set with high priority in execution order
// Used to initialize anything that other scripts are depending on
public class InitialCheck : MonoBehaviour {
    
    private void Start() {
        // Initialize Events System
        EventManager.InitDict();
    }
    
}
