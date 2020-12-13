using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    // public static AudioMixer sfx;
    public Slider musicSlider, sfxSlider;
    private static float musicVol = .5f;
    private static float sfxVol = .5f;
    private static int sceneID;

    void Awake()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;
    }

    void Update()
    {
        if (sceneID != SceneManager.GetActiveScene().buildIndex)
        {
            sceneID = SceneManager.GetActiveScene().buildIndex;
            musicSlider.value = musicVol;
            sfxSlider.value = sfxVol;
        }
    }

    public void SetLevel(float slideValue)
    {
        musicVol = slideValue;
        mixer.SetFloat("MusicVolume", Mathf.Log10(slideValue) * 20f);
    }

    public void SetLevelSFX(float slideValue)
    {
        // sfxVol = slideValue;
        // sfx.SetFloat("Exposed param", Mathf.Log10(slideValue) * 20f);
    }

}
