using UnityEngine;

public class AudioManager : MonoBehaviour
{


    [Header("------ Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource blobSFX;



    [Header("------ Audio Clip ---------")]
    public AudioClip background;
    public AudioClip corgiBite;
    public AudioClip corgiHeal;
    public AudioClip corgiScratch;
    //public AudioClip dragonRoar;
    public AudioClip dragonAttack;
    public AudioClip blobAttack;


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

    public void PlayBlobSFX(AudioClip clip)
    {
        blobSFX.PlayOneShot(clip);
    }
}