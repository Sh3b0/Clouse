using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Transform _oldParent;
    public GameObject keyIcon;
    private readonly Queue<GameObject> _icons = new Queue<GameObject>();
    private Rigidbody me;
    private bool frozenL, frozenR;
    
    private void Start()
    {
        me = GetComponent<Rigidbody>();
        _oldParent = transform.parent;
    }

    private void Update()
    {
        if (frozenL && Input.GetAxis("Horizontal") > 0)
        {
            UnFreeze();
            frozenL = false;
        }

        if (frozenR && Input.GetAxis("Horizontal") < 0)
        {
            UnFreeze();
            frozenR = false;
        }
    }

    private void UnFreeze()
    {
        me.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionZ;
        Player.playerActive = true;
        CameraController.cameraActive = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        keyIcon.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || !Player.playerActive || !Player.isGrounded || frozenL || frozenR) return;

        // Play animation of the player holding the box if not currently playing.
        

        if (Input.GetButton("Interact")) // If player currently holding the box
        {
            keyIcon.SetActive(false);
            transform.parent = Player.me;
            Player.isMovingBox = true;
            Player.jumpingEnabled = false;
        }
        else // If player left the box, while staying in holding range.
        {
            LeaveBox(this);
            keyIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) // If player left the box and the holding range.
    {
        if (!other.CompareTag("Player")) return;
        LeaveBox(this);
    }
    public static void LeaveBox(Box box)
    {
        // Stop any box holding animation.
        Player.isMovingBox = false;
        box.keyIcon.SetActive(false);
        box.transform.parent = box._oldParent;
        Player.jumpingEnabled = true;
    }
    private void OnCollisionEnter(Collision other)
    {
        /* If a box (faller) falls on top of another box (fallee)
            disable the faller triggers
            make the fallee a parent of the faller
            make the base box icon points to the most recent added box.
        */

        float y1 = transform.position.y, y2 = other.collider.transform.position.y;
        if (!(other.collider.CompareTag("Box") && Mathf.Abs(y1 - y2) > 0.01f)) return;

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

        // Get icons and append them to a queue.
        GameObject fallerIcon = faller.GetComponentsInChildren(typeof(Transform), true)[1].gameObject;
        GameObject falleeIcon = fallee.GetComponentsInChildren(typeof(Transform), true)[1].gameObject;
        _icons.Enqueue(falleeIcon);
        _icons.Enqueue(fallerIcon);

        // Fallee became parent of faller.
        faller.transform.parent = fallee.transform;

        // The base icon change place.
        _icons.Peek().transform.position = fallerIcon.transform.position;
    }

    private void OnCollisionStay(Collision other)
    {
        Vector3 boxPos = transform.position;
        if (other.collider.CompareTag("Wall"))
        {
            // print("Box colliding with a wall");
            me.constraints = RigidbodyConstraints.FreezeAll;
            transform.parent = _oldParent;
            
            Player.playerActive = false;
            CameraController.cameraActive = false;
            
            if (other.collider.transform.position.x < boxPos.x) // LeftWall
            {
                frozenL = true;
                boxPos.x += 0.1f;
            }
            else // RightWall
            {
                frozenR = true;
                boxPos.x -= 0.1f;
            }
            transform.position = boxPos;
        }
        else if (other.collider.CompareTag("Player"))
        {
            Vector3 playerPos = Player.me.position;
            if (playerPos.x > boxPos.x) playerPos.x = boxPos.x + 1.5f;
            else playerPos.x = boxPos.x - 1.5f;
            Player.me.position = playerPos;
        }
    }
}