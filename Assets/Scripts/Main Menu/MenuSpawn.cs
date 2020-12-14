using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class MenuSpawn : MonoBehaviour
{

    public GameObject eFlyingEye;
    public GameObject eGoblin;
    public GameObject eMushroom;
    public GameObject eSkeleton;
    public Tilemap groundTiles;

    public GameObject doorFlyingEye;
    public GameObject doorGoblin;
    public GameObject doorMushroom;
    public GameObject doorSkeleton;
    /*
     * Flying Eye = 0
     * Goblin = 1
     * Mushroom = 2
     * Skeleton = 3
     */
    private int currentEnemy = 0;
    private bool enemySpawn = true;
    private GameObject endDestination;
    void Start()
    {
        doorFlyingEye.SetActive(true);
        doorGoblin.SetActive(false);
        doorMushroom.SetActive(false);
        doorSkeleton.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if(EnemyBehavior.EnemyList.Count == 0)
        {
            enemySpawn = true;
        }

        if (enemySpawn)
        {
            SpawnEnemy();
            enemySpawn = false;
        }
    }

    private void SpawnEnemy()
    {
        if (currentEnemy == 0)
        {
            doorSkeleton.SetActive(false);
            doorFlyingEye.SetActive(true);
            SpawnFlyingEyeEnemy();
            currentEnemy++;
        }
        else if(currentEnemy == 1)
        {
            doorFlyingEye.SetActive(false);
            doorGoblin.SetActive(true);
            SpawnGoblinEnemy();
            currentEnemy++;
        }
        else if(currentEnemy == 2)
        {
            doorGoblin.SetActive(false);
            doorMushroom.SetActive(true);
            SpawnMushroomEnemy();
            currentEnemy++;
        }
        else
        {
            doorMushroom.SetActive(false);
            doorSkeleton.SetActive(true);
            SpawnSkeletonmEnemy();
            currentEnemy = 0;
        }
    }

    #region Spawn Enemies
    private void SpawnFlyingEyeEnemy()
    {
        GameObject e = GameObject.Instantiate(eFlyingEye) as GameObject;
        PickDesintation(1);

        // Starting pos
        e.transform.position = groundTiles.GetCellCenterWorld(new Vector3Int(-8, UnityEngine.Random.Range(-1, 0), 0));
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnGoblinEnemy()
    {
        GameObject e = GameObject.Instantiate(eGoblin) as GameObject;
        PickDesintation(1);

        // Starting pos
        e.transform.position = groundTiles.GetCellCenterWorld(new Vector3Int(-8, UnityEngine.Random.Range(-2, 0), 0));
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnMushroomEnemy()
    {
        GameObject e = GameObject.Instantiate(eMushroom) as GameObject;
        PickDesintation(1);

        // Starting pos
        e.transform.position = groundTiles.GetCellCenterWorld(new Vector3Int(-8, UnityEngine.Random.Range(-2, 0), 0));
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnSkeletonmEnemy()
    {
        GameObject e = GameObject.Instantiate(eSkeleton) as GameObject;
        PickDesintation(1);

        // Starting pos
        e.transform.position = groundTiles.GetCellCenterWorld(new Vector3Int(-8, UnityEngine.Random.Range(-2, 0), 0));
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }
    #endregion

    private void PickDesintation(int endpoint)
    {
        endDestination = GameObject.Find("Endpoint " + endpoint.ToString());
    }
}
