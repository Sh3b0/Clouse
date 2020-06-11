using DigitalRuby.Lightning;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveSpeed, lightSpeed;
    public ParticleSystem Rain;
    public GameObject lightningRayPrefab, lightningEffectPrefab;
    public SkillBarController SkillBar;

    public enum Mode
    {
        IDLE = 0, RAIN = 1, LIGHTNING = 2, ICE = 3
    };

    public static Mode state = Mode.IDLE;

    private void Start()
    {
        Lightning.StartObject = gameObject;
    }

    void FixedUpdate()
    {
        if (Player.cloudActive) // Moving the cloud if active
        {
            transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * moveSpeed, transform.position.y);
        }
    }

    private void Update()
    {
        if (!Player.cloudActive) return;

        // 1 - Rain
        if (state == Mode.IDLE && Input.GetButtonDown("Rain"))
        {
            state = Mode.RAIN; // Play some rain sound/animation.
            Rain.Play();
            SkillBar.ActivateSkill(0); // TODO Change magic number to constant ot hash code
        }
        else if (state == Mode.RAIN && Input.GetButtonDown("Rain"))
        {
            state = Mode.IDLE; // Stop the raining sound/animation
            Rain.Stop(); 
            SkillBar.DeactivateSkill(0);
        }

        
        // 2 - Lightening
        if (state == Mode.IDLE && Input.GetButtonDown("Lightening"))
        {
            state = Mode.LIGHTNING; // Play some lightning sound/animation (One Shot)

            GameObject LightningRay = Instantiate(lightningRayPrefab);
            GameObject LightningEffect = Instantiate(lightningEffectPrefab, transform.position + Vector3.down, Quaternion.identity);

            if (Generator.Ready) // If a generator is nearby
            {
                Lightning.EndPosition = Generator.position;
                GameObject.FindWithTag("Icon").GetComponent<SpriteRenderer>().color = Color.green;
            }
            else // fire lightning randomly
            {
                Lightning.EndPosition = new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10), -10, 0);
            }

            Destroy(LightningEffect, 0.1f);
            Destroy(LightningRay, 0.1f);

            state = Mode.IDLE;
        }
        
        // 3 - Ice, TODO: Visualize
        if (state == Mode.IDLE && Input.GetButtonDown("Ice"))
        {
            state = Mode.ICE; // Play some ice sound/animation.
            SkillBar.ActivateSkill(2);
        }
        else if (state == Mode.ICE && Input.GetButtonDown("Ice"))
        {
            state = Mode.IDLE; // Stop the ice sound/animation
            SkillBar.DeactivateSkill(2);
        }

    }
}
