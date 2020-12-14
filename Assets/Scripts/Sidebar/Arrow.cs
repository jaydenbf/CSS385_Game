using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public EnemySpawnSystem e;
    public int difficulty = 0;
    public int position = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(difficulty == 0)
        {
            difficulty0();

        } else if(difficulty == 1)
        {
            difficulty1();
        }
    }

    public void difficulty0()
    {
        if(position == 0)
        {
            difficulty0position0();

        } else if(position == 1)
        {
            difficulty0position1();
        }
    }

    public void difficulty0position0()
    {
        if (GameManager.round == 0)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 1)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 2)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 3)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 4)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 5)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 6)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public void difficulty0position1()
    {
        if (GameManager.round == 0)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 1)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 2)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 3)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 4)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 5)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 6)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public void difficulty1()
    {
        if (position == 0)
        {
            difficulty1position0();

        }

        else if (position == 1)
        {
            difficulty1position1();

        } else if(position == 2)
        {
            difficulty1position2();
        }
    }

    public void difficulty1position0()
    {
        if (GameManager.round == 0)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 1)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 2)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 3)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 4)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 5)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else if (GameManager.round == 6)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public void difficulty1position1()
    {
        if (GameManager.round == 0)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 1)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 2)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 3)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 4)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 5)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 6)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public void difficulty1position2()
    {
        if (GameManager.round == 0)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 1)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 2)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 3)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 4)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        else if (GameManager.round == 5)
        {
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.color = new Color(0f, 0f, 0f, 0f);
        }

        else if (GameManager.round == 6)
        {
            if (e.spawnWave)
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(0f, 0f, 0f, 0f);
            }

            else
            {
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                r.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
