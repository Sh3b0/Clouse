﻿using UnityEngine;

public class Box : MonoBehaviour
{
    public float speed;
    public AudioSource Audio;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Lake"))
        {
            Audio.Play(); // box falls down
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Player.playerActive)
        {
            // Some animation of the player holding the box
            GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0));
            // transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal"), transform.position.y, transform.position.z);
        }
    }
}