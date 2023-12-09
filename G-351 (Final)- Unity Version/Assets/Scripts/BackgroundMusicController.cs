using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioClip explorationMusic;
    public AudioClip fightingMusic;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayExplorationMusic();
    }

    public void PlayExplorationMusic()
    {
        audioSource.clip = explorationMusic;
        audioSource.Play();
    }

    public void PlayFightingMusic()
    {
        audioSource.clip = fightingMusic;
        audioSource.Play();
    }
}
