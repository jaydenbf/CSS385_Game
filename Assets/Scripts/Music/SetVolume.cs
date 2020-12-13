using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixer sfx;

    public void SetLevel(float slideValue)
    {
        // Call dont destroy on load to save values
        DontDestory.musicVol = slideValue;
        mixer.SetFloat("MusicVolume", Mathf.Log10(slideValue) * 20f);
    }

    public void SetLevelSFX(float slideValue)
    {
        // sfx.SetFloat("Exposed param", Mathf.Log10(slideValue) * 20f);
    }
}
