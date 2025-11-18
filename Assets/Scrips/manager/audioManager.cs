using UnityEngine;

public class audioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private static audioManager _instance;

    public static audioManager Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    [Header("Audio Sources")]

    public AudioSource musicBackground;
    public AudioSource musicBackgroundMenu;
    public AudioSource buttonSound;
    public AudioSource gunSound;

    public void PlayMusicBackgroundMenu(AudioClip clip)
    {
        musicBackgroundMenu.PlayOneShot(clip);
    }

    public void PlayMusicSource(AudioClip clip)
    {
        musicBackground.PlayOneShot(clip);
    }

    public void PlayGunSound(AudioClip clip)
    {
        gunSound.PlayOneShot(clip);
    }



}
