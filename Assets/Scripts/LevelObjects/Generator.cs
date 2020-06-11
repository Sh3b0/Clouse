using UnityEngine;
public class Generator : MonoBehaviour
{
    public static bool Ready = false;
    public static Vector3 position;
    private void Start()
    {
        position = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cloud"))
            Ready = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cloud")) Ready = false;
    }
}