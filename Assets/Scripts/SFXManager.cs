using UnityEngine;

public class SFXManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip buttonClicked;
    [SerializeField]
    private AudioClip explosion;
    [SerializeField]
    private AudioClip wheelRotate;

    private static SFXManager instance;
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
            Debug.LogError("More than one SFXManager");
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayButtonClicked()
    {
        audioSource.PlayOneShot(instance.buttonClicked);
    }

    public static void PlayExplosion()
    {
        audioSource.PlayOneShot(instance.explosion);
    }
}