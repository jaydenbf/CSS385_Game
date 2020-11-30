using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRangeUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TowerUI t = this.GetComponentInParent<TowerUI>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (t.isColliding)
        {
            Color c = Color.red;
            sr.color = c;
        }
        else
        {
            Color c = new Color(255, 255, 255, 255);
            sr.color = c;
        }

        float s = gameObject.GetComponentInParent<TowerUI>().tower.range * 2;
        //float s = gameObject.GetComponentInParent<Tower1>().range * 2;
        Vector3 v = new Vector3(s, s, 0);
        transform.localScale = v;
    }
}