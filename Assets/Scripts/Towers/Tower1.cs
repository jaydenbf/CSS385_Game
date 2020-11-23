using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]
    public string towerName = "Tower";
    public float range = 15f;
    public float attackSpeed = 1f;
    public int damage = 50;

    [Header("Other")]
    private float fireCooldown = 0f;

    public string enemyTag = "Enemy";

    public GameObject Projectile;
    public Transform firePoint;

    public int cost = 10;

    private GameManager gmanager;

    // Start is called before the first frame update
    void Start()
    {
        // run the UpdateTarget method every 0.5s
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        // set the firepoint to the center of the tower
        firePoint = transform;

        gmanager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;


        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / attackSpeed;
        }

        fireCooldown -= Time.deltaTime;
    }

    // when tower is selected
    private void OnDrawGizmosSelected()
    {
        // draw tower range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // find closest target
    void UpdateTarget ()
    {
        // find all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // test all enemies and find the closest
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (DistanceToEnemy < shortestDistance && enemy.GetComponent<EnemyBehavior>().alive)
            {
                shortestDistance = DistanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bulletObject = (GameObject)Instantiate(Projectile, firePoint.position, firePoint.rotation);
        Projectile bullet = bulletObject.GetComponent<Projectile>();
        if (bullet != null)
        {
            bullet.setTarget(target);
            bullet.SetDamage(damage);
        }
    }

    private void OnMouseDown()
    {
        gmanager.SelectTower(this);
    }

    public string getTowerInfo()
    {
        return (towerName + "\n" +
            "Damage: " + damage + "\n" +
            "Range: " + range + "\n" +
            "Attack Speed: " + attackSpeed);
    }
}
