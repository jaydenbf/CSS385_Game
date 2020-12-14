using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if(GameManager.selectedTower != null)
        {
            int cash = GameManager.cash;
            int upgradeCost = GameManager.selectedTower.towerUpgrade.cost;

            if (cash >= upgradeCost)
            {
                Color c = new Color32(255, 255, 255, 128);
            } else
            {
                Color c = new Color32(191, 191, 191, 255);
            }
        }
    }
}
