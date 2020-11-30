using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Transform towerTransform;

    // Start is called before the first frame update
    void Start()
    {
        towerTransform = gameObject.GetComponentInParent<Transform>();

        transform.position = towerTransform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (towerTransform == null)
            Destroy(this);
    }
}
