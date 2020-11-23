using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelect : MonoBehaviour
{
    public SpriteRenderer rangeIndicator;
    public GameObject ui;
    public Text towerDetails;
    private Tower1 tower;


    public void SetTarget(Tower1 tower_in)
    {
        tower = tower_in;

        transform.position = tower.transform.position;

        towerDetails.text = tower.getTowerInfo();

        //set range indicator size... this is not exact
        rangeIndicator.transform.localScale = new Vector2(tower.range * .8f, tower.range * .8f);
        rangeIndicator.enabled = true;
        ui.SetActive(true);
    }

    public void Hide()
    {
        rangeIndicator.enabled = false;
        ui.SetActive(false);
    }

}
