using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveSpeed, lightSpeed;
    public ParticleSystem Rain;
    public GameObject LighteningBallPrefab;
    public SkillBarController SkillBar;
    
    public enum Mode
    {
        IDLE = 0, RAIN = 1, LIGHTINING = 2, ICE = 3
    };

    public static Mode state = Mode.IDLE;

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
        if (state == Mode.IDLE && Input.GetButtonUp("Rain"))
        {
            state = Mode.RAIN;
            print("Raining"); // Play some rain sound/animation.
            Rain.Play();
            SkillBar.ActivateSkill(0); // TODO Change magic number to constant ot hash code
        }
        else if (state == Mode.RAIN && Input.GetButtonUp("Rain"))
        {
            state = Mode.IDLE;
            Rain.Stop(); // Stop the raining sound/animation
            SkillBar.DeactivateSkill(0);
        }

        // 2 - Lightening
        if (state == Mode.IDLE && Input.GetButtonUp("Lightening"))
        {
            state = Mode.LIGHTINING;

            print("Lightening"); // Play some lightening sound/animation (One Shot)

            // Instantiate a Thunder Ball that hits the target if in range.
            GameObject LighteningBall = Instantiate(LighteningBallPrefab, transform.position + Vector3.down, Quaternion.identity);

            if (Generator.Ready) // If a generator is nearby
            {
                Vector3 dir = (Generator.position - LighteningBall.transform.position).normalized;
                LighteningBall.GetComponent<Rigidbody>().AddForce(lightSpeed * dir, ForceMode.Impulse);
            }
            else // do lightening randomly
            {
                LighteningBall.GetComponent<Rigidbody>().AddForce(lightSpeed * Vector3.down, ForceMode.Impulse);
                Destroy(LighteningBall, 0.1f);
            }

            state = Mode.IDLE;
        }

        // 3 - ICE
        if (state == Mode.IDLE && Input.GetButtonUp("Ice"))
        {
            state = Mode.ICE;
            print("Ice"); // Play some ice sound/animation.
            SkillBar.ActivateSkill(2);
        }
        else if (state == Mode.ICE && Input.GetButtonUp("Ice"))
        {
            state = Mode.IDLE;
            // Stop the ice sound/animation
            SkillBar.DeactivateSkill(2);
        }

    }
}
