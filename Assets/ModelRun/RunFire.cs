using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunFire : MonoBehaviour
{
    float speed = -20f;
    float destroyDistance = 2200f;
    Rigidbody2D myRigi;
    Vector3 startpos;

    void Start()
    {
        myRigi = GetComponent<Rigidbody2D>();
        myRigi.velocity = transform.right * speed;
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - startpos).sqrMagnitude;
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
