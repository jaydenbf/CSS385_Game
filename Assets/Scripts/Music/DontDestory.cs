using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DontDestory : MonoBehaviour
{
    public static float musicVol;
    public static float sfxVol;

    private int sceneID;
    private bool updateSliderOnce = false;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        sceneID = SceneManager.GetActiveScene().buildIndex;
        updateSliderOnce = false;
        musicVol = 0.5f;
        sfxVol = 0.5f;
    }

    void Update()
    {
        if(sceneID != SceneManager.GetActiveScene().buildIndex)
        {
            updateSliderOnce = true;
            sceneID = SceneManager.GetActiveScene().buildIndex;
        }

        if (updateSliderOnce)
        {
            // Change slider component to match 
        }
    }
}
