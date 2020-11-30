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
    public int level = 1;
    public string note;

    [Header("Other")]
    private float fireCooldown = 0f;

    [Header("Upgrade Values")]
    public float rangeUpgrade = 2f;
    public float attackSpeedUpgrade = -0.25f;
    public int damageUpgrade = 10;

    public string enemyTag = "Enemy";

    public GameObject Projectile;
    public Transform firePoint;
    public Tower1 towerUpgrade = null;

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

        LookAt(target.transform.position);
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

    private void LookAt(Vector3 targetPos)
    {
        targetPos = target.position;
        Vector3 thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
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
        gmanager.selectionDelay = Time.timeSinceLevelLoad;
    }

    public string getTowerUpgradeInfo()
    {
        return (towerName + "\n" +
            "Level " + level + "\n\n" +
            "Damage: " + damage + "→" + (damage + damageUpgrade) + "\n" +
            "Range: " + range + "→" + (range + rangeUpgrade) + "\n" +
            "Cooldown: " + attackSpeed + "→" + (attackSpeed + attackSpeedUpgrade) + "\n");
    }

    public string getTowerInfo()
    {
        return (towerName + "\n" +
            "Damage: " + damage + "\n" + 
            "Range: " + range + "\n" +
            "Attack Speed: " + attackSpeed + "\n" +
            "Cost: $" + cost + "\n" +
            note);
    }

    public void Upgrade()
    {
        level += 1;
        attackSpeed += attackSpeedUpgrade;
        damage += damageUpgrade;
        range += rangeUpgrade;

        if (level == 2)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if (level == 3)
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public string getSellInfo()
    {
        return ("Sell $" + cost);
    }

    public string getUpgradeInfo()
    {
        if (towerUpgrade != null)
        {
            return ("Upgrade $" + towerUpgrade.cost);
        }
        else
        {
            return ("Fully Upgraded");
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
