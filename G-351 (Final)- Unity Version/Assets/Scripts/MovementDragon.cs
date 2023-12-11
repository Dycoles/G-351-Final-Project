using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDragon : MonoBehaviour
{
    public float impulseForce = 170000.0f;
    public float impulseTorque = 3000.0f;

    Animator animController;
    Rigidbody rigidBody;
    AudioSource audioSource;

    public AudioClip footstepSound; 

    // Variable to check if the corgi is moving
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(0, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isCurrentlyMoving = input.magnitude > 0.001;

        if (isCurrentlyMoving)
        {
            rigidBody.AddRelativeTorque(new Vector3(0, input.y * impulseTorque * Time.deltaTime, 0));
            rigidBody.AddRelativeForce(new Vector3(0, 0, input.z * impulseForce * Time.deltaTime));

            animController.SetBool("Move", true);

            // Play footstep sound only when the corgi starts moving
            if (!isMoving)
            {
                PlayFootstepSound();
            }

            // Set the corgi as moving
            isMoving = true;
        }
        else
        {
            animController.SetBool("Move", false);

            // Stop the footstep sound when the corgi stops moving
            if (isMoving)
            {
                StopFootstepSound();
            }

            // Set the corgi as not moving
            isMoving = false;
        }
    }

    void PlayFootstepSound()
    {
        // Check if the AudioSource component and footstep sound exist
        if (audioSource != null && footstepSound != null && !audioSource.isPlaying)
        {
            // Set the footstep sound to the AudioSource
            audioSource.clip = footstepSound;
            // Play the footstep sound
            audioSource.Play();
        }
    }

    void StopFootstepSound()
    {
        // Stop the footstep sound if it is currently playing
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
