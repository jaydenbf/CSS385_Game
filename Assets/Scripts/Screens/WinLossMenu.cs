using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLossMenu : MonoBehaviour
{
    public Text roundReachUI;
    public Text livesLeftUI;
    public Text totalEnemyKilled;
    public Text flyingEyeCount;
    public Text goblinCount;
    public Text mushroomCount;
    public Text skeletonCount;

    private static int roundReach;
    private static int livesLeft;

    private static int flyingEyeKilled;
    private static int goblinKilled;
    private static int mushroomKilled;
    private static int skeletonKilled;

    void Update()
    {
        ShowText();
    }

    private void ShowText()
    {
        // Get Various Game Statistics from game manager
        roundReach = GameManager.lastRound;
        livesLeft = GameManager.lastLives;
        flyingEyeKilled = GameManager.lastKilledFlyingEye;
        goblinKilled = GameManager.lastKilledGoblin;
        mushroomKilled = GameManager.lastKilledMushroom;
        skeletonKilled = GameManager.lastKilledSkeleton;

        roundReachUI.text = "Round Reached:\t\t\t\t" + roundReach + " / 7";

        // Show Lives Count
        if(livesLeft < 0)
        {
            livesLeft = 0;
        }

        livesLeftUI.text = "Lives Left:\t\t\t\t\t" + livesLeft + " / 100";

        int totalAmount = flyingEyeKilled + goblinKilled + mushroomKilled + skeletonKilled;
        // Show total Enemy kill count
        totalEnemyKilled.text = "Total Enemies Killed:\t\t" + totalAmount;

        // Show Individual Enemy Kill Count
        flyingEyeCount.text = "" + flyingEyeKilled;
        goblinCount.text = "" + goblinKilled;
        mushroomCount.text = "" + mushroomKilled;
        skeletonCount.text = "" + skeletonKilled;

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
