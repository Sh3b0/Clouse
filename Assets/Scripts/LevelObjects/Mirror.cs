using DigitalRuby.Lightning;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject keyIcon;
    public GameObject lightningPrefab;
    public int mode;
    public Animation anim;
    public float[] reflectionAngles;
    public bool dir;
    
    private bool _inRange;
    
    private void Update()
    {
        if (CompareTag("FlashLight") && Player.playerActive && _inRange && Input.GetButtonUp("Interact"))
        {
            Work();
            return;
        }
        // If player in range and pressed 'Interact'
        if (Player.playerActive && _inRange && Input.GetButtonUp("Interact"))
        {
            if (dir)
            {
                mode++;
                if (mode == 3)
                    dir = false;
                anim.Play(mode - 1 + "-" + mode);
            }
            else
            {
                mode--;
                if (mode == 1)
                    dir = true;
                anim.Play(mode + 1 + "-" + mode);
            }
        }
    }

    public void Work()
    {
        GameObject lightning = Instantiate(lightningPrefab);
        Lightning.StartObject = gameObject;

        // Reflection Direction;
        Vector3 reflectionDir = new Vector3(Mathf.Cos(reflectionAngles[mode] * Mathf.Deg2Rad),
            Mathf.Sin(reflectionAngles[mode] * Mathf.Deg2Rad), 0);

        bool ray = Physics.Raycast(new Ray(transform.position, reflectionDir), out var hit);

        Destroy(lightning, 0.1f);

        if (ray)
        {
            //print(hit.collider);
            Vector3 colPos = hit.collider.transform.position;
            Lightning.EndPosition = colPos;
            if (hit.collider.gameObject.CompareTag("Mirror") || hit.collider.gameObject.CompareTag("FixedMirror"))
                hit.collider.gameObject.GetComponent<Mirror>().Invoke("Work", 0.1f);
            else if (hit.collider.gameObject.CompareTag("Generator"))
                hit.collider.gameObject.GetComponent<Generator>().Work();
            else
            {
                //Lightning.EndPosition = new Vector3(Mathf.Min(30 * reflectionDir.x, colPos.x), Mathf.Min(30 * reflectionDir.y, colPos.y)) +transform.position;
                    Lightning.EndPosition = 30 * reflectionDir + transform.position;
            }
        }
        else
            Lightning.EndPosition = 30 * reflectionDir + transform.position;

        Debug.DrawLine(transform.position, Lightning.EndPosition, Color.green, 1f, false);
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