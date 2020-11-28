using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLossMenu : MonoBehaviour
{
    public Text roundReachUI;
    public Text livesLeftUI; 

    private static int roundReach;
    private static int livesLeft;

    void Update()
    {
        ShowText();
    }

    private void ShowText()
    {
        roundReach = GameManager.lastRound;
        livesLeft = GameManager.lastLives;

        roundReachUI.text = "Round Reached:\t\t" + roundReach + " / 7";

        if(livesLeft < 0)
        {
            livesLeft = 0;
        }

        livesLeftUI.text = "Lives Left:\t\t" + livesLeft + " / 100";
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
