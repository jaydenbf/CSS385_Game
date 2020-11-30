using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

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

    private bool freshRestart = true;
    public static int[] waveReward;
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

        // Wave Bonus Rewards
        waveReward = new int[7];
        for (int i = 0; i < 7; i++)
        {
            waveReward[i] = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (freshRestart)
        {
            freshRestart = false;
            ResetGame();
        }

        if (GameManager.round >= 7 && EnemyBehavior.EnemyList.Count == 0)
        {
            Thread.Sleep(750);
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);

            ResetGame();
            freshRestart = true;
            SceneManager.LoadScene("Win Screen");
            return;
        }

        if (GameManager.lives <= 0)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);

            ResetGame();
            freshRestart = true;
            SceneManager.LoadScene("Lose Screen");
            return;
        }

        // Start the Wave
        if (startWave)
        {
            spawnWave = true;
            startWave = false;
            GameManager.newRoundTime = true;
            // Tells enemy amount
            flyingEyeAmount = flyingEyeSpawn[waveCounter];
            goblinAmount = goblinSpawn[waveCounter];
            mushroomAmount = mushroomSpawn[waveCounter];
            skeletonAmount = skeletonSpawn[waveCounter];
        }

        // Does not do anything if the user pauses the wave
        if (!spawnWave)
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
        // Start the Round
        if (spawnWave)
        {
            
            if (WaveEnd() && EnemyBehavior.EnemyList.Count == 0)
            {
                GameManager.cash += waveReward[waveCounter];
                waveCounter++;
                spawnWave = false;
                flyingEyeTimePast = 0f;
                goblinTimePast = 0f;
                mushroomTimePast = 0f;
                skeletonTimePast = 0f;
                GameManager.UpdateRound();

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

    private void ResetGame()
    {
        GameManager.SaveRoundInfo();
        GameManager.lives = 100;
        GameManager.cash = 100;
        GameManager.round = 0;
        EnemyBehavior.EnemyList.Clear();

        GameManager.killedFlyingEye = 0;
        GameManager.killedGoblin = 0;
        GameManager.killedMushroom = 0;
        GameManager.killedSkeleton = 0;
        spawnWave = false;
    }

    public void StartWave()
    {
        if (!spawnWave)
        {
            startWave = true;
        }
    }
    #endregion

    #region Spawn Enemies
    private void SpawnFlyingEye()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(eFlyingEye) as GameObject;

        // Find target designation
        PickDestination(1);

        // Starting pos
        e.transform.position = generateValue(true);
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
        PickDestination(3);

        // Starting pos
        e.transform.position = generateValue(false);
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
        PickDestination(4);

        // Starting pos
        e.transform.position = generateValue(false);
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
        PickDestination(2);

        // Starting pos
        e.transform.position = generateValue(true);
        // e.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        e.transform.rotation = Quaternion.identity;

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void PickDestination(int endpoint)
    {
        endDestination = GameObject.Find("Endpoint " + endpoint.ToString());
    }

    private Vector3 generateValue(bool top)
    {
        // Generate -7f to 5f
        // groundTiles.GetCellCenterWorld(new Vector3Int(-12, UnityEngine.Random.Range(-7f, 5f), 0));

        // Generate -7f to 5f
        Vector3 pos;
        if (top)
            pos = groundTiles.GetCellCenterWorld(new Vector3Int(-12, 4, 0));
        else
            pos = groundTiles.GetCellCenterWorld(new Vector3Int(-12, -6, 0));

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
