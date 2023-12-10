using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDragon : MonoBehaviour
{
    public float impulseForce = 170000.0f;
    public float impulseTorque = 3000.0f;
    public float shootingCooldown = 1f;
    private float lastShotTime;
    public GameObject bulletPrefab;
    public Transform firePoint;

    Animator animController;
    Rigidbody rigidBody;
    AudioSource audioSource; // Added AudioSource

    // Reference to the AudioSource component for gunshot sound
    AudioSource gunshotAudioSource;

    // Reference to the AudioSource component for default background music
    public AudioSource defaultMusicAudioSource; // Drag your default music AudioSource from the Inspector

    // Reference to the AudioSource component for fight track
    public AudioSource fightTrackAudioSource; // Drag your fight track AudioSource from the Inspector

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component

        // Get the AudioSource component for gunshot sound
        gunshotAudioSource = GetComponent<AudioSource>();

        // Set the initial background music
        if (defaultMusicAudioSource != null)
        {
            if (!defaultMusicAudioSource.isPlaying)
            {
                defaultMusicAudioSource.Play();
            }

            // Stop the fight track if it's playing
            if (fightTrackAudioSource != null && fightTrackAudioSource.isPlaying)
            {
                fightTrackAudioSource.Stop();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(0, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.magnitude > 0.001)
        {
            rigidBody.AddRelativeTorque(new Vector3(0, input.y * impulseTorque * Time.deltaTime, 0));
            rigidBody.AddRelativeForce(new Vector3(0, 0, input.z * impulseForce * Time.deltaTime));

            animController.SetBool("Move", true);
        }
        else
        {
            animController.SetBool("Move", false);
        }
    }

    void Shoot()
    {
        // Instantiate the bullet
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Play gunshot sound
        PlayGunshotSound();

        // Start the fight track if not already playing
        StartFightTrack();
    }

    void PlayGunshotSound()
    {
        // Check if the AudioSource component for gunshot sound exists
        if (gunshotAudioSource != null)
        {
            // Play the gunshot sound
            gunshotAudioSource.Play();
        }
    }

    void StartFightTrack()
    {
        // Check if the AudioSource component for fight track exists
        if (fightTrackAudioSource != null)
        {
            // Stop the default music if it's playing
            if (defaultMusicAudioSource != null && defaultMusicAudioSource.isPlaying)
            {
                defaultMusicAudioSource.Stop();
            }

            // Play the fight track if it's not already playing
            if (!fightTrackAudioSource.isPlaying)
            {
                fightTrackAudioSource.Play();
            }
        }
    }
}
