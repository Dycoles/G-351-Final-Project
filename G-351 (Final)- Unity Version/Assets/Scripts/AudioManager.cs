using UnityEngine;

public class AudioManager : MonoBehaviour
{


    [Header("------ Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;



    [Header("------ Audio Clip ---------")]
    public AudioClip background;
    public AudioClip corgiBite;
    public AudioClip corgiHeal;
    public AudioClip corgiScratch;
    //public AudioClip fightMusic;

    // Start is called before the first frame update
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    
    // 
    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}