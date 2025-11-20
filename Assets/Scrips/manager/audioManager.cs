using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioSource gunSound;

    public void SetPlayMusicBackgroundMenu(float volume)
    {
        musicBackgroundMenu.volume = volume;
    }


    public void SetMusicBackgroundVolume(float volume)
    {
        musicBackground.volume = volume;
    }
    
    public void SetMusicBackgroundMenuVolume(float volume)
    {
        musicBackgroundMenu.volume = volume;
    }

    public void SetPlayGunSound(AudioClip clip)
    {
        gunSound.PlayOneShot(clip);
    }

}
