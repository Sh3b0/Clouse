using DigitalRuby.LightningBolt;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveSpeed, lightSpeed;
    public ParticleSystem Rain;
    public GameObject DirectionalLightPrefab, LightningPrefab;

    public enum Mode
    {
        IDLE = 0, RAIN = 1, LIGHTINING = 2, ICE = 3
    };

    public static Mode state = Mode.IDLE;
    private void Start()
    {
        LightningBoltScript.StartObject = gameObject;
    }

    void FixedUpdate()
    {
        if (Player.cloudActive) // Moving the cloud if active
        {
            transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * moveSpeed, transform.position.y);
        }
        if (Player.position.x > transform.position.x)
        {
            transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        }
    }

    private void Update()
    {
        if (!Player.cloudActive) return;

        // 1 - Rain
        if (state == Mode.IDLE && Input.GetButtonDown("Rain"))
        {
            state = Mode.RAIN;
            print("Raining"); // Play some rain sound/animation.
            Rain.Play();
        }
        else if (state == Mode.RAIN && Input.GetButtonDown("Rain"))
        {
            state = Mode.IDLE;
            Rain.Stop(); // Stop the raining sound/animation
        }

        // 2 - Lightening
        if (state == Mode.IDLE && Input.GetButtonDown("Lightening"))
        {
            state = Mode.LIGHTINING;

            print("Lightening"); // Play some lightning sound/animation (One Shot)

            GameObject LightningRay = Instantiate(LightningPrefab);
            GameObject LightningEffect = Instantiate(DirectionalLightPrefab, transform.position + Vector3.down, Quaternion.identity);

            if (Generator.Ready) // If a generator is nearby
            {
                LightningBoltScript.EndPosition = Generator.position;
                GameObject.FindWithTag("Icon").GetComponent<SpriteRenderer>().color = Color.green;
            }
            else // fire lightning randomly
            {
                LightningBoltScript.EndPosition = new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10), -10, 0);  
            }

            Destroy(LightningEffect, 0.1f);
            Destroy(LightningRay, 0.1f);


            /* // Old code

            // Instantiate a Thunder Ball that hits the target if in range.
            
            if (Generator.Ready) // If a generator is nearby
            {
                
            }
            else // do lightening randomly
            {
                
            }
            */

            state = Mode.IDLE;
        }

        // 3 - ICE
        if (state == Mode.IDLE && Input.GetButtonDown("Ice"))
        {
            state = Mode.ICE;
            print("Ice"); // Play some ice sound/animation.
        }
        else if (state == Mode.ICE && Input.GetButtonDown("Ice"))
        {
            state = Mode.IDLE;
            // Stop the ice sound/animation
        }

    }
}
