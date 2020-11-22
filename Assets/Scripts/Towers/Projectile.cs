﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public float AoERadius = 0f;

    private int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        //Tower1 parentTower = this.transform.GetComponentInParent<Tower1>();
        //damage = parentTower.damage;
    }

    // Update is called once per frame
    void Update()
    {
        // if projectile has no target it is destroyed
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // object hit
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    // set the target for the projectile
    public void setTarget(Transform target_in)
    {
        target = target_in;
    }

    void HitTarget()
    {
        if (AoERadius > 0)
        {
            AoEDamage();
        } else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        EnemyBehavior targetEnemy = enemy.GetComponentInParent<EnemyBehavior>();
        targetEnemy.AttackDamageRecieved(damage);
        Debug.Log("Projectile.Damage()");
        // play hit effect (TODO)
    }

    void AoEDamage()
    {
        // collider search is not working, fix (TODO)
        Collider[] colliders = Physics.OverlapSphere(transform.position, AoERadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
}
