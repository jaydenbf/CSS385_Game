using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemySpawnSystem : MonoBehaviour
{
    public float gameTimer = 0f;

    #region Enemy Spawn Amount
    private int[] chickenSpawn;
    private int[] cowSpawn;
    private int[] pigSpawn;
    private int[] llamaSpawn;
    #endregion

    #region Enemy Prefabs
    private GameObject eChicken;
    private GameObject eCow;
    private GameObject ePig;
    private GameObject eLlama;

    private GameObject endDestination;

    #endregion

    #region Enemy Spawn Times
    public float chickenSpawnTime = .5f;
    public float cowSpawnTime = .6f;
    public float pigSpawnTime = .7f;
    public float llamaSpawnTime = .8f;

    private float chickenTimePast = 0f;
    private float cowTimePast = 0f;
    private float pigTimePast = 0f;
    private float llamaTimePast = 0f;
    #endregion

    #region Wave Spawners
    private int waveCounter = 0;
    public int maxWaveCounter = 7;
    public bool startWave = false;
    public bool spawnWave = false;
    public bool pauseWave = false;

    private int chickenAmount;
    private int cowAmount;
    private int pigAmount;
    private int llamaAmount;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        chickenSpawn = new int[maxWaveCounter];
        cowSpawn = new int[maxWaveCounter];
        pigSpawn = new int[maxWaveCounter];
        llamaSpawn = new int[maxWaveCounter];

        // Inital Wave 1 will have 3 of each enemy
        chickenSpawn[0] = 3;
        cowSpawn[0] = 3;
        pigSpawn[0] = 3;
        llamaSpawn[0] = 3;

        // Decide spawn amount
        for (int i = 1; i < maxWaveCounter; i++)
        {
            chickenSpawn[i] = chickenSpawn[i - 1] + Random.Range(5,7);
            cowSpawn[i] = cowSpawn[i - 1] + Random.Range(5, 7);
            pigSpawn[i] = pigSpawn[i - 1] + Random.Range(5, 7);
            llamaSpawn[i] = llamaSpawn[i - 1] + Random.Range(5, 7);
        }

        // Load enemy Object
        eChicken = Resources.Load<GameObject>("Assets/Resources/Prefabs/EnemyObjects/TempEnemyChicken") as GameObject;
        eCow = Resources.Load<GameObject>("Resources/Prefabs/EnemyObjects/TempEnemyCow") as GameObject;
        ePig = Resources.Load<GameObject>("Resources/Prefabs/EnemyObjects/TempEnemyPig") as GameObject;
        eLlama = Resources.Load<GameObject>("Resources/Prefabs/EnemyObjects/TempEnemyLlama") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (startWave)
        {
            spawnWave = true;
            startWave = false;

            // Tells enemy amount
            chickenAmount = chickenSpawn[waveCounter];
            cowAmount = cowSpawn[waveCounter];
            pigAmount = pigSpawn[waveCounter];
            llamaAmount = llamaSpawn[waveCounter];
        }

        // Does not do anything if the user pauses the wave
        if (pauseWave)
        {
            return;
        }

        float time = Time.smoothDeltaTime;
        gameTimer += time;

        // Update Enemy Spawn Time
        chickenTimePast += time;
        cowTimePast += time;
        pigTimePast += time;
        llamaTimePast += time;
        Spawner();
    }

    void Spawner()
    {
        if (spawnWave)
        {
            if (WaveEnd())
            {
                spawnWave = false;
                return;
            }

            // Spawn a chicken enemy
            if (CanChickenSpawn())
            {
                SpawnChicken();
                chickenTimePast = 0f;
            }

            // Spawn a cow enemy
            if (CanCowSpawn())
            {
                SpawnCow();
                cowTimePast = 0f;
            }

            // Spawn a pig enemy
            if (CanPigSpawn())
            {
                SpawnPig();
                pigTimePast = 0f;
            }

            // Spawn a llama enemy
            if (CanLlamaSpawn())
            {
                SpawnLlama();
                llamaTimePast = 0f;
            }
        }
    }

    #region Wave Control Functions
    private bool WaveEnd()
    {
        return (chickenAmount <= 0 && cowAmount <= 0 &&
            pigAmount <= 0 && llamaAmount <= 0);
    }

    // Add function which turns start wave to be true. 
    // Most likely has to be a button input
    // Add a function that pauses and unpauses the game

    #endregion

    #region Spawn Enemies
    private void SpawnChicken()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(eChicken) as GameObject;

        // Find target designation
        PickDestination();

        // Starting pos
        e.transform.position = generateValue(0f, 0f, 1f, 1f);

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnCow()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(eCow) as GameObject;

        // Find target designation
        PickDestination();

        // Starting pos
        e.transform.position = generateValue(0f, 0f, 1f, 1f);

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnPig()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(ePig) as GameObject;

        // Find target designation
        PickDestination();

        // Starting pos
        e.transform.position = generateValue(0f, 0f, 1f, 1f);

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void SpawnLlama()
    {
        // Instantiate Obj
        GameObject e = GameObject.Instantiate(eLlama) as GameObject;

        // Find target designation
        PickDestination();

        // Starting pos
        e.transform.position = generateValue(0f, 0f, 1f, 1f);

        // Set up coords and set target destination
        e.transform.up = Vector3.LerpUnclamped(transform.up, endDestination.transform.position, 1);
        e.GetComponent<DestinationSetter>().target = endDestination.transform;
    }

    private void PickDestination()
    {
        int num = UnityEngine.Random.Range(0, 2);
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

    private Vector3 generateValue(float minX, float maxX, float minY, float maxY)
    {
        float newX = UnityEngine.Random.Range(minX, maxX);
        float newY = UnityEngine.Random.Range(minY, maxY);
        Vector3 pos = new Vector3(newX, newY, -0.5f);   
        return pos;
    }
    #endregion

    #region Check if enenmy can spawn
    private bool CanChickenSpawn()
    {
        return chickenTimePast >= chickenSpawnTime; 
    }

    private bool CanCowSpawn()
    {
        return cowTimePast >= cowSpawnTime;
    }

    private bool CanPigSpawn()
    {
        return pigTimePast >= pigSpawnTime;
    }

    private bool CanLlamaSpawn()
    {
        return llamaTimePast >= llamaSpawnTime;
    }

    #endregion


}
