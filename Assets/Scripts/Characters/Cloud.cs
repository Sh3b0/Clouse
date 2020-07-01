using DigitalRuby.Lightning;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public static Mode state = Mode.Idle;
    public static Transform me;
    
    public float moveSpeed;
    public ParticleSystem rain, snow;
    public SkillBarController skillBar;
    public GameObject lightningPrefab;
    public AudioSource rainSound, lightningSound, windSound;
    private Rigidbody _rigidBody;

    public enum Mode
    {
        Idle = 0,
        Rain = 1,
        Lightning = 2,
        Snow = 3
    }

    private void Start()
    {
        rainSound = GetComponents<AudioSource>()[0];
        lightningSound = GetComponents<AudioSource>()[1];
        windSound = GetComponents<AudioSource>()[2];

        me = transform;
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() // Moving the cloud if active or at left/right of camera
    {
        if (!CameraController.cloudVisible && CameraController.fixY && !CameraController.anim.isPlaying)
        {
            bool sign = transform.position.x < CameraController.me.transform.position.x;
            int dir = sign ? 1 : -1;
            _rigidBody.AddForce(dir * Vector3.right, ForceMode.Impulse);
            if(!windSound.isPlaying) windSound.Play();
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
            case Mode.Idle: // Stop all ongoing effects
            {
                skillBar.Initialize();
                rain.Stop(); 
                snow.Stop();
                if (rainSound.isPlaying) rainSound.Stop();
                if (windSound.isPlaying) windSound.Stop();
                break;
            }
            case Mode.Rain:
            {
                if (!rain.isPlaying) rain.Play();
                break;
            }
            case Mode.Lightning:
            {
                StartCoroutine(skillBar.BlinkSkill(1));
                break;
            }
            case Mode.Snow:
            {
                if (!snow.isPlaying) snow.Play();
                break;
            }
        }

        if (!Player.cloudActive) return;

        // 1 - Rain
        if (Input.GetButtonDown("Rain"))
        {
            if (state == Mode.Idle)
            {
                state = Mode.Rain;
                //rainSound.PlayOneShot(rainSound.clip);
                if(!rainSound.isPlaying) rainSound.Play();
            }
            else if (state == Mode.Rain) state = Mode.Idle;
        }

        // 2 - Lightening
        else if (Input.GetButtonDown("Lightning") && state == Mode.Idle)
        {
            lightningSound.PlayOneShot(lightningSound.clip);

            state = Mode.Lightning;
            GameObject lightning = Instantiate(lightningPrefab);
            Lightning.StartObject = gameObject;
            Debug.DrawRay(transform.position, 30 * Vector3.down, Color.white, 1, false);

            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out var hit))
            {
                Lightning.EndPosition = hit.collider.transform.position;
                if (hit.collider.CompareTag("Mirror") || hit.collider.CompareTag("FixedMirror"))
                {
                    hit.collider.gameObject.GetComponent<Mirror>().Invoke(nameof(Mirror.Work), 0.1f);
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
            if (state == Mode.Idle)
            {
                state = Mode.Snow;
                if(!windSound.isPlaying) windSound.Play();
                //windSound.PlayOneShot(windSound.clip);
            }
            else if (state == Mode.Snow) state = Mode.Idle;
        }
    }
}