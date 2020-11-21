using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool alive = true;
    public int health = 100;

    void Update()
    {
        IsAlive();
    }

    private void IsAlive()
    {
        if (health <= 0)
        {
            // Change later where we deestroy enenmy as walking out. 
            alive = false;
        }

        if (alive)
        {
            //Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void AttackDamageRecieved(int damage)
    {
        health -= damage;
    }

    // Probably have to add a collider


}
