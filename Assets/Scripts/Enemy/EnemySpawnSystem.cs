using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public float gameTimer = 0f;

    public int maxWaveCounter = 6;
    private int[] chickenSpawn;
    private int[] cowSpawn;
    private int[] pigSpawn;
    private int[] llamaSpawm;

    private int waveCounter = 0;
    // Start is called before the first frame update
    void Awake()
    {
        chickenSpawn = new int[maxWaveCounter];
        cowSpawn = new int[maxWaveCounter];
        pigSpawn = new int[maxWaveCounter];
        llamaSpawm = new int[maxWaveCounter];
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.smoothDeltaTime;
    }

    void Spawner()
    {

    }
}
