using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float speed = 0f;
    float destroyDistance = 2000f;
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
        if(distance>destroyDistance)
        {
            Destroy(gameObject);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }*/
}
