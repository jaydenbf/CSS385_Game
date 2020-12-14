using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public EnemySpawnSystem e;

    void Start()
    {
        
    }

    void Update()
    {
        if(e.spawnWave)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        } else
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
