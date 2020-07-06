using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject keyIcon, flash;
    private float reflectionAngle, rotationSpeed = 0.5f;
    private bool inRange, interacting;
    private Vector3 reflectionDir;

    private void Start()
    {
        reflectionAngle = transform.rotation.eulerAngles.z;
    }

    private void Update()
    {
        if (!flash.activeSelf) return;
        
        reflectionDir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * reflectionAngle), Mathf.Sin(Mathf.Deg2Rad * reflectionAngle));
        bool ray = Physics.Raycast(new Ray(transform.position, reflectionDir), out var hit);
        
        // Debugging
        Debug.DrawLine(transform.position, 30 * reflectionDir + transform.position, Color.green, 1f);
        
        if (ray && hit.collider.CompareTag("Enemy")) {
            // Destroy corrupted area or enemy
            var corruption = hit.collider.gameObject.GetComponent<Corruption>();
            if (corruption) corruption.OnDisappear();
            else hit.collider.gameObject.GetComponent<Enemy>().OnLightHit();
        }
        
        // If player in range and pressed 'Interact'
        if (inRange && Input.GetButtonUp("Interact"))
        {
            if (!interacting && !Player.playerActive) return;
            interacting = !interacting;
            if (interacting)
            {
                keyIcon.SetActive(false);
                Player.playerActive = false;
                Player.cloudActive = false;
            }
            else
            {
                keyIcon.SetActive(true);
                Player.playerActive = true;
                Player.cloudActive = false;
            }
        }

        if (!interacting) return;

        float angle = Input.GetAxis("Horizontal") > 0 ? -rotationSpeed : rotationSpeed;
        if (Input.GetButton("Horizontal") &&
            (reflectionAngle < 170 && angle > 0 || reflectionAngle > 10 && angle < 0))
        {
            transform.RotateAround(transform.position, Vector3.forward, angle);
            reflectionAngle += angle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && flash.activeSelf)
        {
            keyIcon.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && flash.activeSelf)
        {
            keyIcon.SetActive(false);
            inRange = false;
        }
    }
}