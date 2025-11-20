
using UnityEngine;
using UnityEngine.UI;


public class settingSound : MonoBehaviour
{

    public Slider  musicBackgroundSlider;
    public Slider  musicMenuSlider;

    void Start()
    {
        float musicVoulme = dataManager.dataMusicBackground;
        musicBackgroundSlider.value = musicVoulme;

        float musicMenuVoulme = dataManager.dataMusicMenu;
        musicMenuSlider.value = musicMenuVoulme;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicBackgroundVolume(float volume)
    {
        audioManager.Instance.SetMusicBackgroundVolume(volume);
        dataManager.dataMusicBackground = volume;
    }
    

 public void SetMusicMenuVolume(float volume)
    {
        audioManager.Instance.SetMusicBackgroundMenuVolume(volume);
        dataManager.dataMusicMenu = volume;
    }


}
