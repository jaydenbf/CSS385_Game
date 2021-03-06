﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum PurchaseState 
    {
        None,
        Defense1,
        Defense2,
        Defense3,
        Defense4
    }

    public static PurchaseState purchaseState;
    public static Tilemap groundTiles;
    private static ArrayList reservedTiles;
    private static int trueReservedTiles;
    private static Vector3Int curTile;

    private static GameObject[] defenses;
    private static Tile canPlace;
    private static Tile cannotPlace;
    private static Tile background;

    public static int lives = 100;
    public static int cash = 200;
    public static int round = 0;
    public static int killedFlyingEye = 0;
    public static int killedGoblin = 0;
    public static int killedMushroom = 0;
    public static int killedSkeleton = 0;

    public static int lastLives;
    public static int lastRound;
    public static int lastKilledFlyingEye;
    public static int lastKilledGoblin;
    public static int lastKilledMushroom;
    public static int lastKilledSkeleton;

    public Text livesUI;
    public Text cashUI;
    public Text roundUI;
    public Text roundNotification;
    public Image roundImage;

    public TowerSelect towerSelectedUI;
    public static Tower1 selectedTower = null;

    private float time = 3f;
    private static bool updateRoundTime = false;
    public static bool newRoundTime = false;
    public float selectionDelay;

    public string currentNotif;
    public bool notRound = false;

    // Start is called before the first frame update
    void Start()
    {
        roundImage.enabled = false;
        roundNotification.enabled = false;
        lastLives = lives;
        lastRound = round;
        selectionDelay = float.MinValue;
        purchaseState = PurchaseState.None;
        groundTiles = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        reservedTiles = new ArrayList();

        // Add lines to tile
        /*string[] lines = System.IO.File.ReadAllLines("Assets/Scripts/ReservedTiles.txt");
        foreach (string line in lines)
        {
            string[] coords = line.Split(',');
            reservedTiles.Add(
                new Vector3Int(int.Parse(coords[0], System.Globalization.NumberStyles.AllowLeadingSign), 
                int.Parse(coords[1], System.Globalization.NumberStyles.AllowLeadingSign), 0));
        }
        trueReservedTiles = lines.Length;*/

        defenses = new GameObject[4];
        defenses[0] = (GameObject)Resources.Load("Prefabs/DefenseObjects/Defense1");
        defenses[1] = (GameObject)Resources.Load("Prefabs/DefenseObjects/Defense2");
        defenses[2] = (GameObject)Resources.Load("Prefabs/DefenseObjects/Defense3");
        defenses[3] = (GameObject)Resources.Load("Prefabs/DefenseObjects/Defense4");
        background = (Tile)Resources.Load("Tilesets/Tiles/Ground Tiles/TileSet_V2_47");
        canPlace = (Tile)Resources.Load("Tilesets/Tiles/Ground Tiles/canPlace");
        // cannotPlace = (Tile)Resources.Load("Tilesets/Tiles/Ground Tiles/cannotPlace");
        curTile = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);

        Debug.Assert(groundTiles != null);
        Debug.Assert(canPlace != null);
        // Debug.Assert(cannotPlace != null);
        Debug.Assert(background != null);

        Vector3 dir = transform.transform.localPosition - transform.position;

    }

    /*private void OnGUI()
    {
        // printing enums is really hard apparently, this was easier
        switch(purchaseState)
        {
            case PurchaseState.None:
                GUI.Label(new Rect(10, 10, 400, 20), "Status: won't place anything");
                break;
            case PurchaseState.Defense1:
                GUI.Label(new Rect(10, 10, 400, 20), "Status: will place Defense 1");
                break;
            case PurchaseState.Defense2:
                GUI.Label(new Rect(10, 10, 400, 20), "Status: will place Defense 2");
                break;
            case PurchaseState.Defense3:
                GUI.Label(new Rect(10, 10, 400, 20), "Status: will place Defense 3");
                break;
            case PurchaseState.Defense4:
                GUI.Label(new Rect(10, 10, 400, 20), "Status: will place Defense 4");
                break;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        updateUI();
        ShowRoundNotification();
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        //Vector3Int tilePos = groundTiles.WorldToCell(worldPoint);

        // UpdateCursorTile(tilePos);

        /*if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log(tilePos.x + " " + tilePos.y);
            if (reservedTiles.Contains(tilePos))
            {
                // Debug.Log("This tile is reserved.");
                towerSelectedUI.Hide();
                purchaseState = PurchaseState.None;
            }
            else if (purchaseState != PurchaseState.None)
            {
                PlaceDefense(tilePos);
                towerSelectedUI.Hide();
            }
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeSinceLevelLoad - selectionDelay >= 0.1)
            {
                towerSelectedUI.Hide();
                selectionDelay = float.MinValue;
            }
        }

        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.M))
        {
            cash = 9999;
        }

        updateUI();
    }

    private void UpdateCursorTile(Vector3Int tilePos)
    {
        // if it's a true reserved tile
        if (isTrueReservedTile(tilePos))
        {
            groundTiles.SetTile(curTile, background);
            return;
        }
        groundTiles.SetTile(curTile, background);
        if (reservedTiles.Contains(tilePos))
        {
            // regular reserved tile
            // groundTiles.SetTile(tilePos, cannotPlace);
        }
        else // free real estate
            groundTiles.SetTile(tilePos, canPlace);
            curTile = tilePos;
    }

    private bool isTrueReservedTile(Vector3Int tilePos)
    {
        for (int i = 0; i < trueReservedTiles; i++)
        {
            if ((Vector3Int)reservedTiles[i] == tilePos)
                return true;
        }
        return false;
    }

    private void PlaceDefense(Vector3Int tilePos)
    {
        switch (purchaseState)
        {
            case PurchaseState.Defense1:
                Instantiate(defenses[0],
                    new Vector3(groundTiles.GetCellCenterWorld(tilePos).x,
                    groundTiles.GetCellCenterWorld(tilePos).y, 0),
                    Quaternion.identity);
                Debug.Log("Placed defense 1");
                break;
            case PurchaseState.Defense2:
                Instantiate(defenses[1],
                    new Vector3(groundTiles.GetCellCenterWorld(tilePos).x,
                    groundTiles.GetCellCenterWorld(tilePos).y, 0),
                    Quaternion.identity);
                Debug.Log("Placed defense 2");
                break;
            case PurchaseState.Defense3:
                Instantiate(defenses[2],
                    new Vector3(groundTiles.GetCellCenterWorld(tilePos).x,
                    groundTiles.GetCellCenterWorld(tilePos).y, 0),
                    Quaternion.identity);
                Debug.Log("Placed defense 3");
                break;
            case PurchaseState.Defense4:
                Instantiate(defenses[3],
                    new Vector3(groundTiles.GetCellCenterWorld(tilePos).x,
                    groundTiles.GetCellCenterWorld(tilePos).y, 0),
                    Quaternion.identity);
                Debug.Log("Placed defense 4");
                break;
            default:
                break;
        }
        reservedTiles.Add(tilePos);
    }

    public void updateUI()
    {
        livesUI.text = lives.ToString();
        cashUI.text = cash.ToString();
        roundUI.text = "Round " + (round + 1) + "/7";
    }

    public void SelectTower(Tower1 tower_in)
    {
        selectedTower = tower_in;
        towerSelectedUI.SetTarget(tower_in);
    }

    public static int GetCash()
    {
        return cash;
    }

    public static void addCash(int cash_in)
    {
        cash += cash_in;
    }

    public static void RemoveCash(int cash_out)
    {
        cash = cash - cash_out;
    }


    public static void UpdateRound()
    {
        round++;
        updateRoundTime = true;
    }

    public static void SaveRoundInfo()
    {
        lastLives = lives;
        lastRound = round;
        lastKilledFlyingEye = killedFlyingEye;
        lastKilledGoblin = killedGoblin;
        lastKilledMushroom = killedMushroom;
        lastKilledSkeleton = killedSkeleton;
    }

    private void ShowRoundNotification()
    {
        if(!updateRoundTime && !newRoundTime)
        {
            roundImage.enabled = false;
            roundNotification.enabled = false;
            return;
        }


        if (updateRoundTime)
        {
            time -= Time.smoothDeltaTime;
            // Show Notification
            roundImage.enabled = true;
            roundNotification.enabled = true;

            roundNotification.text = "Round " + round + " Ended";
            if (time <= 0f)
            {
                // Remove  Notification
                roundImage.enabled = false;
                roundNotification.enabled = false;
                time = 3f;
                updateRoundTime = false;
            }
            return;
        }

        if (newRoundTime)
        {
            time -= Time.smoothDeltaTime;

            // Show Notification
            roundImage.enabled = true;
            roundNotification.enabled = true;
            int tempRound = round + 1;
            roundNotification.text = "Round " + tempRound + " Started";

            if (time <= 0f)
            {
                // Remove Box
                roundImage.enabled = false;
                roundNotification.enabled = false;
                time = 3f;
                newRoundTime = false;
            }
        }

    }
}