using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public int DialogIndex;
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        DialogsManager.Instance.OnDialog(DialogIndex);
        Player.me.GetComponents<AudioSource>()[1].Pause(); // Pause walking sound
        Destroy(gameObject);
    }
}
