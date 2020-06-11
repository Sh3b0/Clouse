using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject KeyIcon;
    private bool moved;
    // public AnimationClip GlassTurns, GlassReturns;

    [System.Obsolete]
    private void Update()
    {
        if (KeyIcon.active)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if(!moved) GetComponent<Animation>().Play("GlassTurns");
                else GetComponent<Animation>().Play("GlassReturns");
                moved = !moved;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        KeyIcon.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        KeyIcon.SetActive(false);
    }

}
