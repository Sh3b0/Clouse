using System;
using UnityEngine;

public class Mirror : MonoBehaviour {
    public GameObject KeyIcon;
        
    private bool moved;

    private void Start() {
        KeyIcon.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("press E to interact"); // Visualize this
            if (!moved && Input.GetButton("Interact"))
            {
                GetComponent<Animation>().Play();
                moved = true;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        KeyIcon.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;
        KeyIcon.SetActive(false);
    }
    
}
