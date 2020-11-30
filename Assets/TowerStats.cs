using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStats : MonoBehaviour
{
    private GameManager gmanager;
    private Tower1 tower;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        gmanager = GameObject.FindObjectOfType<GameManager>();
        tower = null;
        text = gameObject.GetComponent<Text>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!gmanager.towerSelectedUI.ui.activeSelf)
        {
            tower = null;
            text.text = "";
        }
        else
        {
            tower = gmanager.towerSelectedUI.tower;
            text.text = tower.getTowerUpgradeInfo();
        }
    }
}
