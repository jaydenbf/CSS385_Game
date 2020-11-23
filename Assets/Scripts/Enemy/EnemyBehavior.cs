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
            if (alive)
                GameManager.cash += reward;
            alive = false;
            // Change Opacity

            
            EnemyList.Remove(this);
            gameObject.GetComponent<AIPath>().canMove = false;

            // Change AIDestinationSetter to start point
            //ChangeTarget();
            //GameManager.lives -= numberOfLives;

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
        if (health <= 0)
        {
            DestroyEnemy();
            // Enemy destroyed! - add currency to shop (TODO)
        }
    }

    /*
    * 
    * Probably have to add a collider
    *
    */

    private void DestroyEnemy()
    {
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
        Vector3 targetPos1 = GameObject.Find("Top Endpoint").transform.position;
        Vector3 targetPos2 = GameObject.Find("Middle Endpoint").transform.position;
        Vector3 targetPos3 = GameObject.Find("Bottom Endpoint").transform.position;
        float distance = Vector3.Distance(targetPos1, this.transform.position);
        //UnityEngine.Debug.Log(distance);

        // Check if its near the target range 
        if ((targetPos1 - this.transform.position).magnitude <= range
            || (targetPos2 - this.transform.position).magnitude <= range
            || (targetPos3 - this.transform.position).magnitude <= range)
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
}
