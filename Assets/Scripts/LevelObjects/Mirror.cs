using DigitalRuby.Lightning;
using UnityEditor;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject keyIcon;
    public GameObject lightningPrefab;
    public float rotationSpeed;
    private bool _inRange, _interacting;
    [SerializeField] private float _reflectionAngle;

    private void Start()
    {
        _reflectionAngle = transform.eulerAngles.z + 90;
    }

    private void Update()
    {
        // If player in range and pressed 'Interact'
        if (_inRange && Input.GetButtonUp("Interact"))
        {
            if (!_interacting && !Player.playerActive) return;
            _interacting = !_interacting;
            if (_interacting)
            {
                keyIcon.SetActive(false);
                Player.playerActive = false;
                Player.cloudActive = false;
                CameraController.cameraActive = false;
            }
            else
            {
                keyIcon.SetActive(true);
                Player.playerActive = true;
                Player.cloudActive = false;
                CameraController.cameraActive = true;
            }
        }

        if (!_interacting) return;

        float angle = Input.GetAxis("Horizontal") > 0 ? -rotationSpeed : rotationSpeed;
        if (Input.GetButton("Horizontal") &&
            (_reflectionAngle < 170 && angle > 0 || _reflectionAngle > 10 && angle < 0))
        {
            transform.RotateAround(transform.position, Vector3.forward, angle);
            _reflectionAngle += angle;
        }
    }

    public bool mirrorType; // True: left mirror, False: normal mirror

    public void Work()
    {
        GameObject lightning = Instantiate(lightningPrefab);
        Lightning.StartObject = gameObject;

        if (mirrorType)
            _reflectionAngle -= 180;
        

        // Reflection Direction;
        Vector3 reflectionDir = new Vector3(Mathf.Cos(_reflectionAngle * Mathf.Deg2Rad),
            Mathf.Sin(_reflectionAngle * Mathf.Deg2Rad), 0);

        bool ray = Physics.Raycast(new Ray(transform.position, reflectionDir), out var hit);
        
        
        
        Destroy(lightning, 0.1f);

        // Next target
        if (ray && hit.collider.gameObject.CompareTag("Mirror"))
        {
            Lightning.EndPosition = hit.collider.transform.position;
            hit.collider.gameObject.GetComponent<Mirror>().Invoke("Work", 0.1f);
        }
        else if (ray && hit.collider.gameObject.CompareTag("Generator"))
        {
            Lightning.EndPosition = hit.collider.transform.position;
            hit.collider.gameObject.GetComponent<Generator>().Work();
        }
        else
        {
            Lightning.EndPosition = 30 * reflectionDir + transform.position;
        }
        Debug.DrawLine(transform.position, Lightning.EndPosition, Color.green, 0.5f);
        
        if (mirrorType)
            _reflectionAngle += 180;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || CompareTag("FixedMirror")) return;
        _inRange = true;
        keyIcon.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || CompareTag("FixedMirror")) return;
        _inRange = false;
        keyIcon.SetActive(false);
    }
}