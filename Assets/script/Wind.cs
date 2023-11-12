using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    Rigidbody2D myRigi;
    float destroyDistance = 280f;
    Vector3 startpos;
    void Start()
    {
        myRigi = GetComponent<Rigidbody2D>();
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(transform.position.x + 20f, transform.position.y - 5f, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, temp, 20 * Time.deltaTime);
        transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        float distance = (transform.position - startpos).sqrMagnitude;
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }

    }
}
