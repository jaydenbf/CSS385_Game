using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;


public class EnemySpawnSystem : MonoBehaviour
{
    public float gameTimer = 0f;
    public Tilemap groundTiles;
    #region Enemy Spawn Amount
    private int[] flyingEyeSpawn;
    private int[] goblinSpawn;
    private int[] mushroomSpawn;
    private int[] skeletonSpawn;
    #endregion

    #region Enemy Prefabs
    public GameObject eFlyingEye;
    public GameObject eGoblin;
    public GameObject eMushroom;
    public GameObject eSkeleton;

    private GameObject endDestination;

    #endregion

    #region Enemy Spawn Times
    public float flyingEyeSpawnTime = 5f;
    public float goblinSpawnTime = 6f;
    public float mushroomSpawnTime = 7f;
    public float skeletonSpawnTime = 7f;

    public float flyingEyeMinSpawnTime = 2f;
    public float goblinMinSpawnTime = 3f;
    public float mushroomMinSpawmTime = 4f;
    public float skeletonMinSpawnTime = 4f;


    private float flyingEyeTimePast = 0f;
    private float goblinTimePast = 0f;
    private float mushroomTimePast = 0f;
    private float skeletonTimePast = 0f;
    #endregion

    #region Wave Spawners
    public int waveCounter = 0;
    public int maxWaveCounter = 7;
    public bool startWave = false;
    public bool spawnWave = false;
    public bool pauseWave = false;

    private int flyingEyeAmount;
    private int goblinAmount;
    private int mushroomAmount;
    private int skeletonAmount;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        flyingEyeSpawn = new int[maxWaveCounter];
        goblinSpawn = new int[maxWaveCounter];
        mushroomSpawn = new int[maxWaveCounter];
        skeletonSpawn = new int[maxWaveCounter];

        // Inital Wave 1 will have 3 of each enemy
        flyingEyeSpawn[0] = 3;
        goblinSpawn[0] = 3;
        mushroomSpawn[0] = 3;
        skeletonSpawn[0] = 3;

        // Decide spawn amount
        for (int i = 1; i < maxWaveCounter; i++)
        {
            flyingEyeSpawn[i] = flyingEyeSpawn[i - 1] + Random.Range(5,7);
            goblinSpawn[i] = goblinSpawn[i - 1] + Random.Range(5, 7);
            mushroomSpawn[i] = mushroomSpawn[i - 1] + Random.Range(5, 7);
            skeletonSpawn[i] = skeletonSpawn[i - 1] + Random.Range(5, 7);
        }

        // Load enemy Object
    }

    // Update is called once per frame
    void Update()
    {
        if (startWave)
        {
            spawnWave = true;
            startWave = false;

            // Tells enemy amount
            flyingEyeAmount = flyingEyeSpawn[waveCounter];
            goblinAmount = goblinSpawn[waveCounter];
            mushroomAmount = mushroomSpawn[waveCounter];
            skeletonAmount = skeletonSpawn[waveCounter];
        }

        // Does not do anything if the user pauses the wave
        if (pauseWave)
        {
            return;
        }

        float time = Time.smoothDeltaTime;
        gameTimer += time;

        // Update Enemy Spawn Time
        flyingEyeTimePast += time;
        goblinTimePast += time;
        mushroomTimePast += time;
        skeletonTimePast += time;
        Spawner();
    }

    void Spawner()
    {
        if (spawnWave)
        {
            if (WaveEnd())
            {
                waveCounter++;
                spawnWave = false;
                flyingEyeTimePast = 0f;
                goblinTimePast = 0f;
                mushroomTimePast = 0f;
                skeletonTimePast = 0f;
                GameManager.round++;

                AdjustSpawnTimes(1f);
                return;
            }

            // Spawn a chicken enemy
            if (CanFlyingEyeSpawn())
            {
                SpawnFlyingEye();
                flyingEyeTimePast = 0f;
                flyingEyeAmount--;
            }

            // Spawn a cow enemy
            if (CanGoblinSpawn())
            {
                SpawnGoblin();
                goblinTimePast = 0f;
                goblinAmount--;
            }

            // Spawn a pig enemy
            if (CanMushroomSpawn())
            {
                SpawnMushroom();
                mushroomTimePast = 0f;
                mushroomAmount--;
            }

            // Spawn a llama enemy
            if (CanSkeletonSpawn())
            {
                SpawnSkeleton();
                skeletonTimePast = 0f;
                skeletonAmount--;
            }
        }
    }

    #region Wave Control Functions
    private bool WaveEnd()
    {
        return (flyingEyeAmount <= 0 && goblinAmount <= 0 &&
            mushroomAmount <= 0 && skeletonAmount <= 0);
    }

    private void AdjustSpawnTimes(float time)
    {
        if(flyingEyeSpawnTime >= flyingEyeMinSpawnTime)
        {
            flyingEyeSpawnTime -= time;
        }

        if(goblinSpawnTime >= goblinMinSpawnTime)
        {
            goblinSpawnTime -= time;
        }

        if(mushroomSpawnTime >= mushroomMinSpawmTime)
        {
            mushroomSpawnTime -= time;
        }

        if(skeletonSpawnTime >= skeletonMinSpawnTime)
        {
            skeletonSpawnTime -= time;
        }
    }
    // Add function which turns start wave to be true. 
    // Most likely has to be a button input
    // Add a function that pauses and unpauses the game

    #endregion

    #region Spawn Enemies
    private void SpawnFlyingEye()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(eFlyingEye) as GameObject;

        // Find target designation
        PickDestination();

        // Starting pos
        e.transform.position = generateValue();
        //e.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnGoblin()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(eGoblin) as GameObject;

        // Find target designation
        PickDestination();

        // Starting pos
        e.transform.position = generateValue();
        //e.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnMushroom()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(eMushroom) as GameObject;

        // Find target designation
        PickDestination();

        // Starting pos
        e.transform.position = generateValue();
        // e.transform.rotation = Quaternion.identity(0f, 0f, 0f);
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnSkeleton()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(eSkeleton) as GameObject;

        // Find target designation
        PickDestination();

        // Starting pos
        e.transform.position = generateValue();
        // e.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void PickDestination()
    {
        int num = UnityEngine.Random.Range(0, 3);
        if(num == 0)
        {
            endDestination = GameObject.Find("Top Endpoint");
        }
        else if(num == 1)
        {
            endDestination = GameObject.Find("Middle Endpoint");
        }
        else
        {
            endDestination = GameObject.Find("Bottom Endpoint");
        }
    }

    private Vector3 generateValue()
    {
        // Generate -7f to 5f
        // groundTiles.GetCellCenterWorld(new Vector3Int(-12, UnityEngine.Random.Range(-7f, 5f), 0));

        // Generate -7f to 5f
        Vector3 pos = groundTiles.GetCellCenterWorld(new Vector3Int(-12, UnityEngine.Random.Range(-7, 5), 0));
        pos.z = 0f;
        return pos;
    }
    #endregion

    #region Check if enenmy can spawn
    private bool CanFlyingEyeSpawn()
    {
        return flyingEyeTimePast >= flyingEyeSpawnTime; 
    }

    private bool CanGoblinSpawn()
    {
        return goblinTimePast >= goblinSpawnTime;
    }

    private bool CanMushroomSpawn()
    {
        return mushroomTimePast >= mushroomSpawnTime;
    }

    private bool CanSkeletonSpawn()
    {
        return skeletonTimePast >= skeletonSpawnTime;
    }

    #endregion


}
