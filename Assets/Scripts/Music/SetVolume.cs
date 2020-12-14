using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixer sfx;
    public Slider musicSlider, sfxSlider;
    private static float musicVol = .05f;
    private static float sfxVol = .05f;
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
        sfxVol = slideValue;
        sfx.SetFloat("sfxVolume", Mathf.Log10(slideValue) * 20f);
    }

}
