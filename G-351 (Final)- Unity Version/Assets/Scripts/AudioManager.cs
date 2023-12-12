using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource blobSFX;

    [Header("------ Audio Clip ---------")]
    public AudioClip background;
    public AudioClip fightingBackground; // Add a new AudioClip for fighting background music
    public AudioClip corgiBite;
    public AudioClip corgiHeal;
    public AudioClip corgiScratch;
    public AudioClip dragonAttack;
    public AudioClip blobAttack;

    private bool isFighting = false;

    // Start is called before the first frame update
    private void Start()
    {
        PlayBackgroundMusic(); // Start playing the default background music
    }

    // Play the default or fighting background music based on the current state
    private void PlayBackgroundMusic()
    {
        musicSource.clip = isFighting ? fightingBackground : background;
        musicSource.Play();
    }

    // Play SFX for non-background sounds
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Play SFX for blob-related sounds
    public void PlayBlobSFX(AudioClip clip)
    {
        blobSFX.PlayOneShot(clip);
    }

    // Call this method when the player and enemy start fighting
    public void StartFighting()
    {
        isFighting = true;
        PlayBackgroundMusic(); // Switch to the fighting background music
    }

    // Call this method when the fight is over
    public void EndFighting()
    {
        isFighting = false;
        PlayBackgroundMusic(); // Switch back to the default background music
    }
}