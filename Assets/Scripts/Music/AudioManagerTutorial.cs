using UnityEngine;
using UnityEngine.UI;

public class AudioManagerTutorial : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string musicPref = "musicPref";
    private static readonly string sfxPref = "sfxPref";

    private int firstPlayInt;

    public Slider musicSlider, sfxSlider;
    private float musicVolume, sfxVolume;


    // Start is called before the first frame update
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt == 0)
        {
            musicVolume = .25f;
            sfxVolume = .15f;

            musicSlider.value = musicVolume;
            musicSlider.value = sfxVolume;

            PlayerPrefs.SetFloat(musicPref, musicVolume);
            PlayerPrefs.SetFloat(sfxPref, sfxVolume);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            musicVolume = PlayerPrefs.GetFloat(musicPref);
            musicSlider.value = musicVolume;
            sfxVolume = PlayerPrefs.GetFloat(sfxPref);
            sfxSlider.value = sfxVolume;
        }
    }

    void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(musicPref, musicSlider.value);
        PlayerPrefs.SetFloat(sfxPref, musicSlider.value);
    }

    public void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }
}
