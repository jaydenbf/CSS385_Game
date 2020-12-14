using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public float AoERadius = 0f;
    public float attackBounces = 0f;
    public float bounceRange = 4f;
    public string enemyTag = "Enemy";
    public bool isUpgraded = false;
    public bool needsUpgrading = false;
    public GameObject particleEffect;
    ParticleSystem playingParticleEffect;

    public int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        //Tower1 parentTower = this.transform.GetComponentInParent<Tower1>();
        //damage = parentTower.damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (needsUpgrading)
        {
            if (AoERadius > 0)
                AoERadius++;
            if (attackBounces > 0)
                attackBounces++;
            needsUpgrading = false;
            isUpgraded = true;
        }

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
            PlayParticleEffect();
            HitTarget();
            return;
        }

        Vector3 v = target.position - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, 1);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    // set the target for the projectile
    public void setTarget(Transform target_in)
    {
        target = target_in;
    }

    public void SetDamage(int damage_in)
    {
        damage = damage_in;
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

        if (attackBounces > 0)
        {
            attackBounces--;
            FindNewTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Damage(Transform enemy)
    {
        EnemyBehavior targetEnemy = enemy.GetComponentInParent<EnemyBehavior>();
        targetEnemy.AttackDamageRecieved(damage);
    }

    void AoEDamage()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (DistanceToEnemy < AoERadius)
            {
                Damage(enemy.transform);
            }
        }
    }

    private void FindNewTarget()
    {
        // find all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // test all enemies and find the closest
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (DistanceToEnemy < shortestDistance && DistanceToEnemy > .1)
            {
                shortestDistance = DistanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= bounceRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    public void PlayParticleEffect()
    {
        GameObject firework = Instantiate(particleEffect);
        firework.transform.position = this.transform.position;
        firework.GetComponent<ParticleSystem>().Play();
    }
}
