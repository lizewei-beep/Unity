using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    float speed = 0f;
    float destroyDistance = 200f;
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
        //transform.Rotate(new Vector3(0, 0, -135) * Time.deltaTime, Space.World);
        this.transform.rotation = Quaternion.AngleAxis(-135, Vector3.forward);
        float distance = (transform.position - startpos).sqrMagnitude;
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
