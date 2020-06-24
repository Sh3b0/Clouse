using UnityEngine;

public class Player : MonoBehaviour
{
    public static float moveSpeed = 10, jumpForce = 19, gravityScale = 6;
    public static Transform me;
    public static bool playerActive = true, cloudActive, jumpingEnabled = true, isGrounded;
    public static bool isMovingBox;
    public PlayerAnimation anim;
    public SkillBarController skillBar;
    
    private CharacterController _playerPhysics;
    private Vector3 _moveDirection;
    private int _jumpsCount;

    private void Start() {
        isMovingBox = false;
        me = transform;
        skillBar.gameObject.SetActive(false);
        _playerPhysics = GetComponent<CharacterController>();
        _jumpsCount = 0;
    }

    private void Update()
    {
        isGrounded = _playerPhysics.isGrounded;
        // TODO Freeze Controller when game ends
        //if (GameState.IsGameFinished()) return;

        // Switch between player and cloud with Tab key
        if ((playerActive || cloudActive) && Input.GetButtonUp("Switch"))
        {
            playerActive = !playerActive;
            cloudActive = !cloudActive;
            skillBar.gameObject.SetActive(cloudActive);
            if (cloudActive) skillBar.Initialize();
            
            anim.Push(.000f);
            anim.Move(.0f);
        }

        if (playerActive || !_playerPhysics.isGrounded) {
            var horizontalInput = Input.GetAxis("Horizontal");
            _moveDirection = new Vector3(horizontalInput * moveSpeed, _moveDirection.y);

            if (_playerPhysics.isGrounded) _jumpsCount = 0;

            if (jumpingEnabled && _jumpsCount < 2 && Input.GetButtonDown("Jump"))
            {
                _moveDirection = new Vector3(_playerPhysics.velocity.x, jumpForce);
                _jumpsCount++;
                anim.Jump();
            }

            _moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;

            if (!_playerPhysics.enabled) return;
            _playerPhysics.Move(_moveDirection * Time.deltaTime);
            if (isMovingBox) {
                anim.Push(horizontalInput + 0.002f);
                anim.Move(.0f);
            } else {
                anim.Move(horizontalInput);
                anim.Push(.000f);
            }
        }
        else // To prevent moving during dialogs, etc.
        {
            anim.Push(.000f);
            anim.Move(.0f);
        }
    }


    public void Die()
    {
        // TODO Freeze Controller when game ends
        //if (GameState.IsGameFinished()) return;

        _playerPhysics.enabled = false;
        playerActive = false;

        var rb = gameObject.AddComponent<Rigidbody>();

        if (_moveDirection.x >= 0)
        {
            rb.AddForce(Vector3.right * 2, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.left * 2, ForceMode.Impulse);
        }
    }
}
