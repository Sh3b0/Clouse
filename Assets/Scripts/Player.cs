using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed, JumpForce, GravityScale;

    public SkillBarController SkillBar;
    private CharacterController playerPhysics;
    private Vector3 moveDirection;
    private int jumpsCount;
    public static bool playerActive = true, cloudActive = false;

    private void Start()
    {
        SkillBar.gameObject.SetActive(false);
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
            
            // Enable Cloud skill bar
            SkillBar.gameObject.SetActive(cloudActive);
            if (cloudActive) { SkillBar.Initialize(); }
        }

        if (playerActive || !playerPhysics.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, moveDirection.y);

            if (playerPhysics.isGrounded)
            {
                jumpsCount = 0;
            }

            if (Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Vertical"))
            {
                if (jumpsCount < 2)
                {
                    moveDirection = new Vector3(playerPhysics.velocity.x, JumpForce);
                    jumpsCount++;
                }
            }

            moveDirection.y += Physics.gravity.y * GravityScale * Time.deltaTime;

            if (!playerPhysics.enabled) { return; }
            playerPhysics.Move(moveDirection * Time.deltaTime);
        }
    }


    public void Die()
    {
        // TODO Freeze Controller when game ends
        //if (GameState.IsGameFinished()) return;

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
