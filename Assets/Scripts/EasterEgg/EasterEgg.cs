using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag(Constants.TAG_PLAYER)) return;
        SceneManager.LoadScene(Constants.SN_TEST_LEVEL);
    }
    
}
