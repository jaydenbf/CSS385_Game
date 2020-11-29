using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    bool isColliding = false;
    public Tower1 tower;

    void Start()
    {
        
    }

    void Update()
    {
        transform.localPosition = GetMouseWorldPosition();

        if (Input.GetMouseButtonUp(0))
        {
            if (isColliding)
            {
                Destroy(gameObject);
            }
            else
            {
                var temp = Instantiate(tower, GetMouseWorldPosition(), Quaternion.identity);
                GameManager.cash -= temp.GetComponent<Tower1>().cost;
                Destroy(gameObject);
            }
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 v = Input.mousePosition;
        v = Camera.main.ScreenToWorldPoint(v);
        v.z = 0.0f;

        return v;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.r = .85f;
        c.g = .15f;
        c.b = .15f;
        c.a = .5f;
        GetComponent<SpriteRenderer>().color = c;
        isColliding = true;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.r = .85f;
        c.g = .15f;
        c.b = .15f;
        c.a = .5f;
        GetComponent<SpriteRenderer>().color = c;
        isColliding = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.r = 1f;
        c.g = 1f;
        c.b = 1f;
        c.a = 1f;
        GetComponent<SpriteRenderer>().color = c;
        isColliding = false;
    }
}
