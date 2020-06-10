using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed, JumpForce, GravityScale;

    private CharacterController playerPhysics;
    private Vector3 moveDirection;
    private int jumpsCount;

    public static bool playerActive = true, cloudActive = false;
    public AudioSource Walking, Other;
    public AudioClip landing, jumping;

    bool onAir = false;

    private void Start()
    {
        playerPhysics = GetComponent<CharacterController>();
        jumpsCount = 0;
    }

    private void Update()
    {
        // TODO Freeze Controller when game ends
        //if (GameState.IsGameFinished()) return;

        // Switch between boy and cloud with Tab key
        if (Input.GetButtonDown("Switch"))
        {
            playerActive = !playerActive;
            cloudActive = !cloudActive;
        }

        if (playerActive || !playerPhysics.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, moveDirection.y);

            if (playerPhysics.isGrounded && onAir)
            {
                onAir = false;
                print("landing");
                Other.PlayOneShot(landing);
                jumpsCount = 0;
            }

            if (Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Vertical"))
            {
                if (jumpsCount < 2)
                {
                    onAir = true;
                    print("jumping");
                    Other.PlayOneShot(jumping);
                    moveDirection = new Vector3(playerPhysics.velocity.x, JumpForce);
                    jumpsCount++;
                }
            }

            moveDirection.y += Physics.gravity.y * GravityScale * Time.deltaTime;

            if (!playerPhysics.enabled) { return; }


            if (moveDirection.x != 0 && !onAir)
            {
                if(!Walking.isPlaying) Walking.Play();
            }
            else Walking.Stop();

            playerPhysics.Move(moveDirection * Time.deltaTime);
        }
    }


    public void Die()
    {
        // TODO Freeze Controller when game ends
        //if (GameState.IsGameFinished()) return;

        // Play Die sound

        playerPhysics.enabled = false;
        playerActive = false;

        var rb = gameObject.AddComponent<Rigidbody>();

        if (moveDirection.x >= 0)
        {
            rb.AddForce(Vector3.right * 2, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.left * 2, ForceMode.Impulse);
        }
    }
}
