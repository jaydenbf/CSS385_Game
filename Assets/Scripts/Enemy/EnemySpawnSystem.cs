using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


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
    public int maxWaveCounter = 6;
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

        // Inital Wave 1 - 4
        for(int i = 0; i < 4; i++)
        {
            flyingEyeSpawn[i] = 1 + (i * 2);
            goblinSpawn[i] = 1 + (i * 2);
            mushroomSpawn[i] = 1 + (i * 2);
            skeletonSpawn[i] = 1 + (i * 2);
        }

        // Wave 5-6
        for (int i = 4; i < maxWaveCounter; i++)
        {
            flyingEyeSpawn[i] = 7 + Random.Range(0,2);
            goblinSpawn[i] = 7 + Random.Range(0, 2);
            mushroomSpawn[i] = 7 + Random.Range(0, 2);
            skeletonSpawn[i] = 7 + Random.Range(0, 2);
        }

        // Load enemy Object
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            GameManager.round++;

        if (GameManager.round >= 7 && EnemyBehavior.EnemyList.Count == 0)
            SceneManager.LoadScene("Win Screen");
        if (GameManager.lives <= 0)
            SceneManager.LoadScene("Lose Screen");

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
            if (WaveEnd() && EnemyBehavior.EnemyList.Count == 0)
            {
                waveCounter++;
                spawnWave = false;
                flyingEyeTimePast = 0f;
                goblinTimePast = 0f;
                mushroomTimePast = 0f;
                skeletonTimePast = 0f;
                GameManager.cash += GameManager.waveReward[GameManager.round++];

                AdjustSpawnTimes(1f);
                return;
            }
            else if (WaveEnd())
            {
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
