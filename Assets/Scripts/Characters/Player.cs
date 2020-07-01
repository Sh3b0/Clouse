using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool playerActive = true, cloudActive, isMovingBox;
    public static GameObject me;

    public PlayerAnimation anim;
    public SkillBarController skillBar;
    public AudioClip jumping, landing;

    private const float MoveSpeed = 10, JumpForce = 19;
    private CharacterController playerController;
    private AudioSource walking, effects;
    private Vector3 _moveDirection;
    private int _jumpsCount;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        effects = GetComponents<AudioSource>()[0];
        walking = GetComponents<AudioSource>()[1];
        me = gameObject;
    }

    private void Update()
    {
        // TODO Freeze Controller when game ends
        //if (GameState.IsGameFinished()) return;

        // Switch between player and cloud with Tab key
        if ((playerActive || cloudActive) && Input.GetButtonUp("Switch"))
        {
            if (playerActive)
            {
                playerActive = false;
                cloudActive = true;
            }
            else
            {
                playerActive = true;
                cloudActive = false;
            }

            skillBar.gameObject.SetActive(cloudActive);
            if (cloudActive)
            {
                skillBar.Initialize();
                walking.Pause();
            }

            anim.Push(.000f);
            anim.Move(.0f);
        }

        var horizontalInput = Input.GetAxis("Horizontal");

        if (playerActive || !playerController.isGrounded || isMovingBox)
        {
            if (_jumpsCount == 0 && Mathf.Abs(horizontalInput) > 0.001) walking.UnPause();
            else walking.Pause();
        }

        if (playerActive || !playerController.isGrounded)
        {
            _moveDirection = new Vector3(horizontalInput * MoveSpeed, _moveDirection.y);

            if (playerController.isGrounded && _jumpsCount > 0)
            {
                effects.PlayOneShot(landing);
                _jumpsCount = 0;
            }

            if (_jumpsCount < 2 && Input.GetButtonDown("Jump"))
            {
                effects.PlayOneShot(jumping);
                _jumpsCount++;
                _moveDirection = new Vector3(playerController.velocity.x, JumpForce);
                anim.Jump();
            }

            if (!playerController.isGrounded) _moveDirection.y += Physics.gravity.y * Time.deltaTime;

            if (playerController.enabled)
            {
                playerController.Move(_moveDirection * Time.deltaTime);
            }

            if (isMovingBox)
            {
                // Pushing animation should be played even if player stands (with minimum speed)
                anim.Push(horizontalInput + 0.002f);
                anim.Move(.0f);
            }
            else
            {
                anim.Move(horizontalInput);
                anim.Push(.000f);
            }
        }
        else // To prevent moving during dialogs, etc.
        {
            if (isMovingBox)
            {
                // Pushing animation should be played even if player stands (with minimum speed)
                anim.Push(horizontalInput + 0.002f);
                anim.Move(.0f);
            }
        }
    }

    // Used for level restarting
    public void MoveToPoint(Transform point)
    {
        playerController.enabled = false;
        transform.position = point.position;
        playerController.enabled = true;
    }

    public void Die()
    {
        // TODO Freeze Controller when game ends
        //if (GameState.IsGameFinished()) return;

        playerController.enabled = false;
        playerActive = false;

        // Drop the dead player's body
        var rb = gameObject.AddComponent<Rigidbody>();
        if (_moveDirection.x >= 0)
            rb.AddForce(Vector3.right * 2, ForceMode.Impulse);
        else
            rb.AddForce(Vector3.left * 2, ForceMode.Impulse);
    }
}