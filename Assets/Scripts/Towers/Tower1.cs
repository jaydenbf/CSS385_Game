﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : MonoBehaviour
{
    public bool isSelected = false;
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
    public int damageUpgrade;
    public float rangeUpgrade;
    public string customUpgradeCode;
    public int[] upgradeCost;

    public string[] upgradeCodes;

    public string enemyTag = "Enemy";

    public GameObject Projectile;
    public Transform firePoint;
    public Tower1 towerUpgrade = null;
    public AudioClip soundEffect;

    public int cost = 10;
    public int sellValue; 
    private GameManager gmanager;
    private GameObject upgrade;

    public List<Projectile> ProjectileList = new List<Projectile>();
    // Start is called before the first frame update
    void Start()
    {
        // run the UpdateTarget method every 0.5s
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        // set the firepoint to the center of the tower
        firePoint = transform;

        upgrade = (GameObject)Resources.Load("Prefabs/DefenseObjects/Upgrade");
        gmanager = GameObject.FindObjectOfType<GameManager>();

        upgradeCodes = customUpgradeCode.Split(',');
        sellValue = cost - 20;
        // shootSoundEffect = Instantiate(soundEffect);
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
        Vector3 pos = new Vector3(0, 0, 0);
        AudioSource.PlayClipAtPoint(soundEffect, pos);
    }

    private void OnMouseDown()
    {
        gmanager.SelectTower(this);
        gmanager.selectionDelay = Time.timeSinceLevelLoad;
    }

    public string getTowerUpgradeInfo()
    {
        if (level == 3)
        {
            return (towerName + "\n" +
            "Level " + level + "\n\n" +
            "Damage: " + damage + "\n" +
            "Range: " + range + "\n");
        }

        level++;


        bool noDamage = false;
        bool noRange = false;
        bool radius = false;
        bool bounce = false;


        foreach (string s in upgradeCodes)
        {
            if (s == "nodamage" + level.ToString())
                noDamage = true;
            if (s == "norange" + level.ToString())
                noRange = true;
            if (s == "radius" + level.ToString())
                radius = true;
            if (s == "bounce" + level.ToString())
                bounce = true;
        }

        

        string str = towerName + "\n" + "Level " + --level + "\n\n";
        if (noDamage)
            str += "Damage: " + damage + "\n";
        else
            str += "Damage: " + damage + "->" + (damage + damageUpgrade) + "\n";

        if (noRange)
            str += "Range: " + range + "\n";
        else
            str += "Range: " + range + "->" + (range + rangeUpgrade) + "\n";

        if (radius)
        {
            float temp = Projectile.GetComponent<Projectile>().AoERadius;
            str += "Radius: " + temp + "->" + ++temp + "\n";
        }

        if (bounce)
        {
            float temp = Projectile.GetComponent<Projectile>().attackBounces;
            str += "Bounces: " + temp + "->" + ++temp + "\n";
        }

        return str;
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
        sellValue += (upgradeCost[level -1] / 2);

        bool noDamage = false;
        bool noRange = false;
        bool radius = false;
        bool bounce = false;

        foreach (string str in upgradeCodes)
        {
            if (str == "nodamage" + level.ToString())
                noDamage = true;
            if (str == "norange" + level.ToString())
                noRange = true;
            if (str == "radius" + level.ToString())
                radius = true;
            if (str == "bounce" + level.ToString())
                bounce = true;
        }

        if (!noDamage)
            damage += damageUpgrade;
        if (!noRange)
            range += rangeUpgrade;
        if (radius || bounce)
            Projectile.GetComponent<Projectile>().needsUpgrading = true;

        if (level == 2)
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if (level == 3)
        {
            GameObject.Instantiate(upgrade, gameObject.GetComponent<Transform>());
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public string getSellInfo()
    {
        return ("Sell $" + sellValue);
    }

    public string getUpgradeInfo(bool canUpgrade)
    {
        if (canUpgrade)
            return ("Upgrade($" + upgradeCost[level] + ")");
        else
            return ("Max Upgrades");
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
