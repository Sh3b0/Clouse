using DigitalRuby.Lightning;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public static GameObject me;

    public ParticleSystem rain, snow;
    public GameObject lightningPrefab;
    public SkillBarController skillBar;
    public AudioSource rainSound, lightningSound, snowSound;
    public GameObject idleFace, sadFace, angryFace, coldFace;

    private const float MoveSpeed = 15;
    private Rigidbody _rigidBody;
    private Mode state = Mode.Idle;

    private enum Mode
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
        snowSound = GetComponents<AudioSource>()[2];
        _rigidBody = GetComponent<Rigidbody>();
        me = gameObject;
    }

    private void FixedUpdate() // Moving the cloud if active or at left/right of camera
    {
        if (!CameraController.cloudVisible && CameraController.fixY && !CameraController.anim.isPlaying)
        {
            bool sign = transform.position.x < CameraController.me.transform.position.x;
            int dir = sign ? 1 : -1;
            _rigidBody.AddForce(dir * Vector3.right, ForceMode.Impulse);
        }
    }

    private void GoIdle()
    {
        state = Mode.Idle;
        skillBar.Initialize();
        idleFace.SetActive(true);
        sadFace.SetActive(false);
        angryFace.SetActive(false);
        coldFace.SetActive(false);
        rain.Stop();
        rainSound.Stop();
        snow.Stop();
        snowSound.Stop();
    }

    private void Update()
    {
        if (!Player.cloudActive)
        {
            GoIdle();
            return;
        }

        transform.position += new Vector3(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime, 0.0f, 0.0f);

        if (state != Mode.Idle) skillBar.ActivateSkill((int) state - 1);
        else GoIdle();

        // 1 - Rain
        if (Input.GetButtonDown("Rain"))
        {
            if (state == Mode.Idle)
            {
                state = Mode.Rain;
                rain.Play();
                rainSound.Play();
                idleFace.SetActive(false);
                sadFace.SetActive(true);
            }
            else if (state == Mode.Rain) GoIdle();
        }

        // 2 - Lightening
        else if (Input.GetButtonDown("Lightning") && state == Mode.Idle)
        {
            if (LevelsManager.CurrentLevel < 3) return;
            idleFace.SetActive(false);
            angryFace.SetActive(true);
            state = Mode.Lightning;
            Invoke(nameof(GoIdle), 0.5f);
            lightningSound.PlayOneShot(lightningSound.clip);

            GameObject lightning = Instantiate(lightningPrefab);
            Lightning.StartObject = gameObject;

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
                else if (hit.collider.CompareTag("FlashLight"))
                {
                    hit.collider.gameObject.GetComponent<FlashLight>().flash.SetActive(true);
                }
            }
            else // Lightning hit nothing
            {
                Lightning.EndPosition = -50 * Vector3.down;
            }

            Destroy(lightning, 0.1f);
        }

        // 3 - Ice
        else if (Input.GetButtonDown("Ice"))
        {
            if (LevelsManager.CurrentLevel < 5) return;
            if (state == Mode.Idle)
            {
                state = Mode.Snow;
                snow.Play();
                snowSound.Play();
                idleFace.SetActive(false);
                coldFace.SetActive(true);
            }
            else if (state == Mode.Snow) GoIdle();
        }
    }
}