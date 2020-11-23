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
    public Button sellButton;
    public Text sellButtonText;
    public Tower1 tower;
    private GameManager gmanager;

    void Start()
    {
        sellButton.onClick.AddListener(ClickToSell);
        gmanager = GameObject.FindObjectOfType<GameManager>();
        ui.SetActive(false);
    }


    public void SetTarget(Tower1 tower_in)
    {
        tower = tower_in;

        transform.position = tower.transform.position;

        towerDetails.text = tower.getTowerInfo();
        sellButtonText.text = tower.getSellInfo();

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

    public void ClickToSell()
    {
        // refund cash
        gmanager.addCash(tower.cost);
        tower.cost = 0;
        tower.Destroy();

        Hide();
    }
}
