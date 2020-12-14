using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public Slider slider;

    void Update()
    {
        if(slider.value <= 0)
        {
            slider.gameObject.SetActive(false);
        }
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

}
