using DigitalRuby.Lightning;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveSpeed;
    public ParticleSystem rain;
    public SkillBarController skillBar;
    public GameObject lightningPrefab;
    private Rigidbody _rigidBody;
    public static Mode state = Mode.Idle;
    public static Transform me; // TODO: find another way

    public enum Mode
    {
        Idle = 0,
        Rain = 1,
        Lightning = 2,
        Ice = 3
    }

    private void Start()
    {
        me = transform;
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() // Moving the cloud if active or not seen by camera
    {
        if (!CameraController.cloudVisible)
        {
            bool sign = transform.position.x < CameraController.me.transform.position.x;
            int dir = sign ? 1 : -1;
            _rigidBody.AddForce(dir * Vector3.right, ForceMode.Impulse);
        }

        if (!Player.cloudActive) return;

        Vector3 newPosition = transform.position;
        newPosition.x += Input.GetAxis("Horizontal") * moveSpeed;
        transform.position = newPosition;
    }

    private void Update()
    {
        if (!Player.cloudActive) state = Mode.Idle;
        
        if (state != Mode.Idle) skillBar.ActivateSkill((int) state - 1);
        switch (state)
        {
            case Mode.Idle:
            {
                skillBar.Initialize();
                rain.Stop(); // Stop all ongoing effects
                break;
            }
            case Mode.Rain:
            {
                if (!rain.isPlaying) rain.Play(); // Play some rain sound/animation.
                break;
            }
            case Mode.Lightning:
            {
                StartCoroutine(skillBar.BlinkSkill(1));
                break;
            }
            case Mode.Ice:
            {
                // TODO: Visualize Ice.
                break;
            }
        }

        if (!Player.cloudActive) return;
        
        // 1 - Rain
        if (Input.GetButtonDown("Rain"))
        {
            if (state == Mode.Idle) state = Mode.Rain;
            else if (state == Mode.Rain) state = Mode.Idle;
        }

        // 2 - Lightening
        else if (Input.GetButtonDown("Lightning") && state == Mode.Idle)
        {
            state = Mode.Lightning; // Play some lightning sound/animation (One Shot)
            GameObject lightning = Instantiate(lightningPrefab);
            Lightning.StartObject = gameObject;
            Debug.DrawRay(transform.position, 30 * Vector3.down, Color.white, 1, false);
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out var hit))
            {
                Lightning.EndPosition = hit.collider.transform.position;
                if (hit.collider.CompareTag("Mirror") || hit.collider.CompareTag("FixedMirror")
                ) // If lightning hit a mirror
                {
                    print(hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<Mirror>().Invoke("Work", 0.1f);
                }
                else if (hit.collider.CompareTag("Generator"))
                {
                    hit.collider.gameObject.GetComponent<Generator>().Work();
                }
            }
            else // Lightning hit nothing
            {
                Lightning.EndPosition = -10 * Vector3.down;
            }

            Destroy(lightning, 0.1f);
        }

        // 3 - Ice
        else if (Input.GetButtonDown("Ice"))
        {
            if (state == Mode.Idle) state = Mode.Ice;
            else if (state == Mode.Ice) state = Mode.Idle;
        }
    }
}