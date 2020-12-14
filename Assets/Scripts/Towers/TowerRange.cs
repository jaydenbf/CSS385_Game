using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        bool b = gameObject.GetComponentInParent<Tower1>().isSelected;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (b)
        {
            Color c = new Color(255, 255, 255, 255);
            sr.color = c;
        }
        else
        {
            Color c = new Color(255, 255, 255, 0);
            sr.color = c;
        }

        float s = gameObject.GetComponentInParent<Tower1>().range * 2;
        Vector3 v = new Vector3(s, s, 0);
        transform.localScale = v;
    }
}