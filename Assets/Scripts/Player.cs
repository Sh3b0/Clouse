using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed, JumpForce, GravityScale;

    private CharacterController _playerPhysics;
    private Vector3 _moveDirection;
    private int _jumpsCount;

    public static bool playerActive = true, cloudActive = false;

    private void Start()
    {
        _playerPhysics = GetComponent<CharacterController>();
        _jumpsCount = 0;
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

        if (playerActive || !_playerPhysics.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, _moveDirection.y);

            if (_playerPhysics.isGrounded)
            {
                _jumpsCount = 0;
            }

            if (Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Vertical"))
            {
                if (_jumpsCount < 2)
                {
                    _moveDirection = new Vector3(_playerPhysics.velocity.x, JumpForce);
                    _jumpsCount++;
                }
            }

            _moveDirection.y += Physics.gravity.y * GravityScale * Time.deltaTime;

            if (!_playerPhysics.enabled) { return; }
            _playerPhysics.Move(_moveDirection * Time.deltaTime);
        }
    }


    public void Die()
    {
        // TODO Freeze Controller when game ends
        //if (GameState.IsGameFinished()) return;

        _playerPhysics.enabled = false;

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
