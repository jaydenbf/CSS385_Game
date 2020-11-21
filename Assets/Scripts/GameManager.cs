using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    // Start is called before the first frame update
    void Start()
    {
        purchaseState = PurchaseState.None;
        groundTiles = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        reservedTiles = new ArrayList();

        // Add lines to tile
        string[] lines = System.IO.File.ReadAllLines("Assets/Scripts/ReservedTiles.txt");
        foreach (string line in lines)
        {
            string[] coords = line.Split(',');
            reservedTiles.Add(
                new Vector3Int(int.Parse(coords[0], System.Globalization.NumberStyles.AllowLeadingSign), 
                int.Parse(coords[1], System.Globalization.NumberStyles.AllowLeadingSign), 0));
        }
        trueReservedTiles = lines.Length;

        defenses = new GameObject[4];
        defenses[0] = (GameObject)Resources.Load("Prefabs/DefenseObjects/Defense1");
        defenses[1] = (GameObject)Resources.Load("Prefabs/DefenseObjects/Defense2");
        defenses[2] = (GameObject)Resources.Load("Prefabs/DefenseObjects/Defense3");
        defenses[3] = (GameObject)Resources.Load("Prefabs/DefenseObjects/Defense4");
        background = (Tile)Resources.Load("Tilesets/Tiles/Ground Tiles/TileSet_V2_47");
        canPlace = (Tile)Resources.Load("Tilesets/Tiles/Ground Tiles/canPlace");
        cannotPlace = (Tile)Resources.Load("Tilesets/Tiles/Ground Tiles/cannotPlace");
        curTile = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);

        Debug.Assert(groundTiles != null);
        Debug.Assert(canPlace != null);
        Debug.Assert(cannotPlace != null);
        Debug.Assert(background != null);
    }

    private void OnGUI()
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
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int tilePos = groundTiles.WorldToCell(worldPoint);

        UpdateCursorTile(tilePos);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(tilePos.x + " " + tilePos.y);
            if (reservedTiles.Contains(tilePos))
            {
                Debug.Log("This tile is reserved.");
                //purchaseState = PurchaseState.None;
            }
            else if (purchaseState != PurchaseState.None)
            {
                PlaceDefense(tilePos);
            }
        }
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
        if (reservedTiles.Contains(tilePos)) // regular reserved tile
            groundTiles.SetTile(tilePos, cannotPlace);
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
}