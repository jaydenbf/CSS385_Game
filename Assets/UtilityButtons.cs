using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilityButtons : MonoBehaviour
{
    private GameManager gmanager;
    private Button button;
    private Text buttonText;
    public EnemySpawnSystem es;

    // Start is called before the first frame update
    void Start()
    {
        gmanager = GameObject.FindObjectOfType<GameManager>();
        button = gameObject.GetComponent<Button>();
        buttonText = button.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (button.name == "Play")
            PlayButtonUpdate();
        else if (!gmanager.towerSelectedUI.ui.activeSelf)
            NotSelected();
        else if (button.name == "Sell")
            ButtonRed();
        else if (gmanager.towerSelectedUI.tower.level == 3)
            NotSelected();
        else if (GameManager.cash >= gmanager.towerSelectedUI.tower.upgradeCost[gmanager.towerSelectedUI.tower.level])
            CanAfford();
        else
            CannotAfford();
    }

    public void PlayButtonUpdate()
    {
        ColorBlock colors = button.colors;
        if (es.WaveEnd())
        {
            colors.normalColor = new Color32(50, 207, 23, 150);
            colors.highlightedColor = new Color32(31, 143, 11, 255);
            button.colors = colors;
            buttonText.text = "PLAY";
        }
        else
        {
            colors.normalColor = new Color32(134, 219, 141, 150);
            colors.highlightedColor = new Color32(80, 171, 88, 255);
            button.colors = colors;
            buttonText.text = "In Progress";
        }
    }

    public void CanAfford()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = new Color32(134, 219, 141, 150);
        colors.highlightedColor = new Color32(80, 171, 88, 255);
        button.colors = colors;
    }
    public void CannotAfford()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = new Color32(156, 152, 152, 150);
        colors.highlightedColor = new Color32(97, 95, 95, 255);
        button.colors = colors;
    }

    public void ButtonRed()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = new Color32(222, 120, 113, 255);
        colors.highlightedColor = new Color32(176, 65, 65, 255);
        button.colors = colors;
    }

    public void NotSelected()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = new Color32(193, 194, 134, 255);
        colors.highlightedColor = new Color32(172, 173, 81, 255);
        button.colors = colors;
    }
}
