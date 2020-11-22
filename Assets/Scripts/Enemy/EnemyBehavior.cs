﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class EnemyBehavior : MonoBehaviour
{
    public bool alive = true;
    public int health = 100;
    public DestinationSetter targetpos;

    void Awake()
    {
        targetpos = GetComponent<DestinationSetter>();
    }

    void Update()
    {
        this.transform.rotation = Quaternion.identity;
        IsAlive();

        // Gets within the point with 26.1f. IDK why, but its that number
        if (InRange(26.1f))
        {
            UnityEngine.Debug.Log("Entering Kill Rnange");
            DestroyEnemy();
        }

        if(InRange(26.1f) && !alive)
        {
            DestroyEnemy();
        }
    }

    private void IsAlive()
    {
        // Enemy is deafeated
        if (health <= 0 || !alive)
        {
            alive = false;
            // Change Opacity

            // Change AIDestinationSetter to start point
            ChangeTarget();
        }
    }

    public void AttackDamageRecieved(int damage)
    {
        // Takes damage when in a certain range
        if (InRange(30f))
        {
            health -= damage;
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
         Destroy(this.gameObject);
    }

    private void ChangeTarget()
    {
        GameObject startPoint = GameObject.Find("Destroypoint");
        targetpos.target = startPoint.transform;
    }

    private bool InRange(float range)
    {
        Vector3 targetPos1 = GameObject.Find("Top Endpoint").transform.position;
        Vector3 targetPos2 = GameObject.Find("Middle Endpoint").transform.position;
        Vector3 targetPos3 = GameObject.Find("Bottom Endpoint").transform.position;
        float distance = Vector3.Distance(targetPos1, this.transform.position);
        UnityEngine.Debug.Log(distance);

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
