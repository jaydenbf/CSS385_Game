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
    public Button upgradeButton;
    public Text upgradeButtonText;
    public Tower1 tower;
    private GameManager gmanager;

    void Start()
    {


        gmanager = GameObject.FindObjectOfType<GameManager>();
        ui.SetActive(false);
    }

    void Update()
    {
        if (tower == null || towerDetails == null)
        {
            sellButtonText.text = "Sell";
            upgradeButtonText.text = "Upgrade";
            return;
        }
        towerDetails.text = tower.getTowerInfo();
        sellButtonText.text = tower.getSellInfo();
        if (tower.level == 3)
            upgradeButtonText.text = tower.getUpgradeInfo(false);
        else
            upgradeButtonText.text = tower.getUpgradeInfo(true);
    }


    public void SetTarget(Tower1 tower_in)
    {
        if (tower == null)
        {
            tower = tower_in;
            tower.isSelected = true;
        }
        else
        {
            Vector3 v = GetMouseWorldPosition();
            if(v.x <= 7.5)
            {
                tower.isSelected = false;
                tower = tower_in;
                tower.isSelected = true;
            }
        }

        //tower = tower_in;

        transform.position = tower.transform.position;

        //set range indicator size... this is not exact
        //rangeIndicator.transform.localScale = new Vector2(tower.range * .8f, tower.range * .8f);
        //rangeIndicator.enabled = true;
        ui.SetActive(true);
    }

    public void Hide()
    {
        Vector3 v = GetMouseWorldPosition();
        if (v.x > 7.5)
            return;
        //rangeIndicator.enabled = false;
        ui.SetActive(false);

        if (tower != null)
        {
            
            if(v.x <= 7.5)
            {
                tower.isSelected = false;
                tower = null;
            }

            //towerDetails.text = "";
            sellButtonText.text = "Sell";
            upgradeButtonText.text = "Upgrade";
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 v = Input.mousePosition;
        v = Camera.main.ScreenToWorldPoint(v);
        v.z = 0.0f;

        return v;
    }

    public void ClickToSell()
    {
        if (tower == null)
        {
            ui.SetActive(false);
            return;
        }

        // refund cash
        GameManager.addCash(tower.cost);
        tower.cost = 0;
        tower.Destroy();
        tower = null;
        ui.SetActive(false);
        
        Hide();
    }

    public void ClickToUpgrade()
    {
        if (tower == null)
            return;
        if (GameManager.cash >= tower.upgradeCost[tower.level - 1] && tower.level < 3)
        {
            tower.Upgrade();
            GameManager.cash -= tower.upgradeCost[tower.level - 1];
        }
    }

}
