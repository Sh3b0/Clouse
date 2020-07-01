using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject keyIcon;
    public float speed;
    public AudioSource boxFallsDown;
    public bool isMetal;

    private bool stuckL, stuckR;
    private GameObject player;
    private Rigidbody playerRigidBody;
    private BoxCollider playerCollider;
    private CharacterController playerController;

    private void Start()
    {
        player = Player.me;
        playerRigidBody = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<BoxCollider>();
        playerController = player.GetComponent<CharacterController>();
        keyIcon = GetComponentsInChildren(typeof(Transform), true)[1].gameObject;
    }

    private void Update()
    {
        if (Player.isMovingBox && CompareTag("Held"))
        {
            float inp = Input.GetAxis("Horizontal");
            if (inp < 0 && !stuckL || inp > 0 && !stuckR)
            {
                transform.position += new Vector3(inp * speed * Time.deltaTime, 0.0f, 0.0f);
                player.transform.position += new Vector3(inp * speed * Time.deltaTime, 0.0f, 0.0f);
                stuckL = stuckR = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            keyIcon.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || Player.cloudActive) return;
        
        // if player is not facing box, then can't interact
        if ((PlayerAnimation.dir && player.transform.position.x > transform.position.x ||
                                     !PlayerAnimation.dir && player.transform.position.x < transform.position.x))
        {
            keyIcon.SetActive(false);
            return;
        }

        if (Input.GetButton("Interact") && playerController.isGrounded) // If player currently holding the box
        {
            keyIcon.SetActive(false);
            Player.isMovingBox = true;
            Player.playerActive = false;
            playerController.enabled = false;
            playerCollider.enabled = true;
            if (playerRigidBody == null)
            {
                playerRigidBody = player.AddComponent<Rigidbody>();
                playerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            }

            tag = "Held";
        }
        else // If player left the box, while staying in holding range.
        {
            LeaveBox(true);
        }
    }

    private void OnTriggerExit(Collider other) // If player left the box and the holding range.
    {
        if (other.CompareTag("Player"))
            LeaveBox(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 boxPos = transform.position, colPos = other.collider.transform.position;
        float x1 = boxPos.x, x2 = colPos.x, y1 = boxPos.y, y2 = colPos.y;

        if (other.collider.CompareTag("Ground"))
        {
            boxFallsDown.Play();
            return;
        }

        if (other.collider.CompareTag("Wall") || other.collider.CompareTag("Stand") ||
            ((other.collider.CompareTag("Box") || other.collider.CompareTag("Held")) && Mathf.Abs(x1 - x2) >= 1))
        {
            if (x1 < x2) stuckR = true;
            else stuckL = true;
            return;
        }

        if (!other.collider.CompareTag("Box")) return;
        LeaveBox(false);

        /*
         If a box (faller) falls on top of another box (fallee)
            disable the faller triggers
            make the fallee a parent of the faller
            make the base box icon points to the most recent added box.
        */

        GameObject faller, fallee;
        if (y1 < y2)
        {
            faller = other.collider.gameObject;
            fallee = gameObject;
        }
        else
        {
            fallee = other.collider.gameObject;
            faller = gameObject;
        }

        // Disable interacting with the faller.
        faller.GetComponent<BoxCollider>().enabled = false;
        faller.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameObject fallerIcon = faller.GetComponentsInChildren(typeof(Transform), true)[1].gameObject;
        fallerIcon.SetActive(false);

        // Fallee became parent of faller.
        faller.transform.parent = fallee.transform;

        // The base icon change place.
        keyIcon = fallerIcon;
    }

    private void LeaveBox(bool iconState)
    {
        if (!player) return;
        if (!Player.playerActive && !Player.cloudActive) Player.playerActive = true;
        Player.isMovingBox = false;
        playerController.enabled = true;
        playerCollider.enabled = false;
        Destroy(playerRigidBody);
        tag = "Box";
        keyIcon.SetActive(iconState);
    }
}