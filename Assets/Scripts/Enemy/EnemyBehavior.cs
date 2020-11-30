using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using UnityEngine.Tilemaps;

public class EnemyBehavior : MonoBehaviour
{
    public bool alive = true;
    public int health = 100;
    public DestinationSetter targetpos;
    public Healthbar healthBar;
    public int reward;
    public int numberOfLives;
    public Animator animator;
    public static List<EnemyBehavior> EnemyList = new List<EnemyBehavior>();

    public int enemyName;

    private bool callDestoryOnce = true;

    void Start()
    {
        targetpos = GetComponent<DestinationSetter>();
        healthBar.SetMaxHealth(health);
        healthBar.gameObject.SetActive(false);
        EnemyList.Add(this);
    }

    void Update()
    {
        IsAlive();
        if (!alive)
            return;
        //animator.SetInteger("Health", health);
        this.transform.rotation = Quaternion.identity;
       
        if(InRange(1f) || !alive)
        {
            GameManager.lives -= numberOfLives;
            EnemyList.Remove(this);

            DestroyEnemyFast();
        }


        Debug.Log("Number of Enemies: " + EnemyList.Count);
    }

    private void IsAlive()
    {
        // Enemy is deafeated
        if (health <= 0 || !alive)
        {
            if (callDestoryOnce)
            {
                DestroyEnemyCounter();
                callDestoryOnce = false;
            }
                
            if (alive)
                GameManager.cash += reward;
            alive = false;
            // Change Opacity

            
            EnemyList.Remove(this);
            gameObject.GetComponent<AIPath>().canMove = false;

            DestroyEnemy();
        }
    }

    public void AttackDamageRecieved(int damage)
    {
        // Takes damage when in a certain range
        if (InRange(100f))
        {
            health -= damage;
            healthBar.SetHealth(health);
            healthBar.gameObject.SetActive(true);
        }
    }


    private void DestroyEnemyCounter()
    {
        //FlyingEye = 0,     // == 0
        //Goblin = 1,     // == 1
        //Mushroom = 2,     // == 2
        //Skeleton = 3     // == 3
        if (enemyName == 0)
        {
            GameManager.killedFlyingEye++;
        }
        else if(enemyName == 1)
        {
            GameManager.killedGoblin++;
        }
        else if(enemyName == 2)
        {
            GameManager.killedMushroom++;

        }
        else
        {
            GameManager.killedSkeleton++;
        }
    }

    private void DestroyEnemy()
    {
        // Update which enemy is destoryed
        UnityEngine.Debug.Log("Destroying Enemy");

        // Destroy(transform.parent.gameObject);
        
        EnemyList.Remove(this);
        animator.SetTrigger("Death");
        Destroy(this.gameObject, 0.75f);
    }

    private void DestroyEnemyFast()
    {
        UnityEngine.Debug.Log("Destroying Enemy");

        // Destroy(transform.parent.gameObject);

        EnemyList.Remove(this);
        Destroy(this.gameObject);
    }

    //private void ChangeTarget()
    //{
    //    GameObject startPoint = GameObject.Find("Destroypoint");
    //    targetpos.target = startPoint.transform;
    //}

    private bool InRange(float range)
    {
        Vector3 targetPos1 = GameObject.Find("Endpoint 1").transform.position;
        Vector3 targetPos2 = GameObject.Find("Endpoint 2").transform.position;
        Vector3 targetPos3 = GameObject.Find("Endpoint 3").transform.position;
        Vector3 targetPos4 = GameObject.Find("Endpoint 4").transform.position;
        float distance = Vector3.Distance(targetPos1, this.transform.position);
        //UnityEngine.Debug.Log(distance);

        // Check if its near the target range 
        if ((targetPos1 - this.transform.position).magnitude <= range
            || (targetPos2 - this.transform.position).magnitude <= range
            || (targetPos3 - this.transform.position).magnitude <= range
            || (targetPos4 - this.transform.position).magnitude <= range)
        {
            return true;
        }
        return false;
    }

    private bool InRangeStart(float range)
    {
        Vector3 targetPos4 = GameObject.Find("Destroypoint").transform.position;

        if((targetPos4 - this.transform.position).magnitude <= range){
            return true;
        }

        return false;
    }

    public static EnemyBehavior GetClosestEnemy(Vector3 position, float range)
    {
        EnemyBehavior closest = null;
        float closestDistance = 100;

        for (int i = 0; i < EnemyList.Count; i++)
        {
            EnemyBehavior current = EnemyList[i];
            float currentDistance = Vector3.Distance(position, current.GetPosition());

            if (currentDistance <= range)
            {
                if (closest = null)
                {
                    closest = current;
                    closestDistance = currentDistance;
                }
                else if (currentDistance < closestDistance)
                {
                    closest = current;
                    closestDistance = currentDistance;
                }
            }
        }

        return closest;
    }

    public Vector3 GetPosition()
    {
        return transform.localPosition;
    }
}
