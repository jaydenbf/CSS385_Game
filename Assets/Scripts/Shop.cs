using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public EnemySpawnSystem es;
    public GameManager gmanager;
    public Text sellText;

    private void Start()
    {
        gmanager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gmanager.towerSelectedUI != null && gmanager.towerSelectedUI.ui.activeSelf)
            sellText.text = "Sell ($" + gmanager.towerSelectedUI.tower.cost.ToString() + ")";
        else
            sellText.text = "Sell";
    }

    public void PurchaseDefense1()
    {
        Debug.Log("Defense 1 Purchased");
        if (GameManager.purchaseState == GameManager.PurchaseState.Defense1)
            GameManager.purchaseState = GameManager.PurchaseState.None;
        else
            GameManager.purchaseState = GameManager.PurchaseState.Defense1;
    }

    public void PurchaseDefense2()
    {
        Debug.Log("Defense 2 Purchased");
        if (GameManager.purchaseState == GameManager.PurchaseState.Defense2)
            GameManager.purchaseState = GameManager.PurchaseState.None;
        else
            GameManager.purchaseState = GameManager.PurchaseState.Defense2;
    }

    public void PurchaseDefense3()
    {
        Debug.Log("Defense 3 Purchased");
        if (GameManager.purchaseState == GameManager.PurchaseState.Defense3)
            GameManager.purchaseState = GameManager.PurchaseState.None;
        else
            GameManager.purchaseState = GameManager.PurchaseState.Defense3;
    }

    public void PurchaseDefense4()
    {
        Debug.Log("Defense 4 Purchased");
        if (GameManager.purchaseState == GameManager.PurchaseState.Defense4)
            GameManager.purchaseState = GameManager.PurchaseState.None;
        else
            GameManager.purchaseState = GameManager.PurchaseState.Defense4;
    }

    public void StartWave()
    {
        Debug.Log("Wave begin");
        es.startWave = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Sell()
    {
        if (gmanager.towerSelectedUI != null)
            gmanager.towerSelectedUI.ClickToSell();
    }
}
