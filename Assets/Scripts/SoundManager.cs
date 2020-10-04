using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip buttonClicked;
    [SerializeField]
    private AudioClip explosion;
    [SerializeField]
    private AudioClip wheelRotate;
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private AudioClip gameMusic;

    private static SoundManager instance;
    private static AudioSource audioSource;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("More than one SoundManager");
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayButtonClicked(float volumeScale = 1)
    {
        audioSource.PlayOneShot(instance.buttonClicked, volumeScale);
    }

    public static void PlayExplosion(float volumeScale = 1)
    {
        audioSource.PlayOneShot(instance.explosion, volumeScale);
    }

    public static void PlayWheelRotate(float volumeScale = 1)
    {
        audioSource.PlayOneShot(instance.wheelRotate, volumeScale);
    }

    public static void StopMusic()
    {
        audioSource.clip = null;
    }

    public static void PlayMenuMusic()
    {
        audioSource.clip = instance.menuMusic;
        audioSource.Play();
    }

    public static void PlayGameMusic()
    {
        audioSource.clip = instance.gameMusic;
        audioSource.Play();
    }
}