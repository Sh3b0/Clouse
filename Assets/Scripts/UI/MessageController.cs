using UnityEngine;
using UnityEngine.UI;

// UI Script
// Used to show disappearing messages on the screen
public class MessageController : MonoBehaviour {

    public Text Message;
    public Animation TextAnimation;

    private static MessageController _instance;

    private void Start() {
        _instance = this;
    }

    // For some other messages just add parameter to method
    public static void ShowMessage() {
        if (_instance) {
            _instance.TextAnimation.Play();
        }
    }
    
}
