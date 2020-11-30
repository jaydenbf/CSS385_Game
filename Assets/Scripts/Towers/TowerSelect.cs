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
        sellButton.onClick.AddListener(ClickToSell);
        upgradeButton.onClick.AddListener(ClickToUpgrade);
        gmanager = GameObject.FindObjectOfType<GameManager>();
        ui.SetActive(false);
    }

    void Update()
    {

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

        towerDetails.text = tower.getTowerInfo();
        sellButtonText.text = tower.getSellInfo();
        upgradeButtonText.text = tower.getUpgradeInfo();

        //set range indicator size... this is not exact
        //rangeIndicator.transform.localScale = new Vector2(tower.range * .8f, tower.range * .8f);
        //rangeIndicator.enabled = true;
        ui.SetActive(true);
    }

    public void Hide()
    {
        //rangeIndicator.enabled = false;
        ui.SetActive(false);

        if (tower != null)
        {
            Vector3 v = GetMouseWorldPosition();
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
        // refund cash
        GameManager.addCash(tower.cost);
        tower.cost = 0;
        tower.Destroy();

        Hide();
    }

    public void ClickToUpgrade()
    {
        // this check is not working
        if ((GameManager.GetCash() > tower.towerUpgrade.cost) && (tower.towerUpgrade != null) && tower.level < 3)
        {
            Debug.Log("Cash: " + GameManager.GetCash());
            // remove cash
            GameManager.RemoveCash(tower.towerUpgrade.cost);
            
            tower.Upgrade();

            sellButtonText.text = tower.getSellInfo();
            upgradeButtonText.text = tower.getUpgradeInfo();
            SetTarget(tower);
        }
    }

}
