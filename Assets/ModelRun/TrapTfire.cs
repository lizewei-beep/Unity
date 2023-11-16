using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTfire : MonoBehaviour
{
    public GameObject arrowPrefab;
    int fireMax=5;
    float disPerArrowX;
    float disPerArrowY;

    public bool rightWay;

    bool canFire;
    void Awake()
    {
        canFire = false;
        disPerArrowX = 3f;
        disPerArrowY = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        canFire = false;
        if (rightWay)
        {
            int fireCount=4;
            int i = fireCount % 5;
            Vector3 temp = new Vector3(transform.position.x + 27f + disPerArrowX * (5-i), transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
            i = fireCount % 5;
            temp = new Vector3(transform.position.x + 27f + disPerArrowX * (5 - i), transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
            i = fireCount % 5;
            temp = new Vector3(transform.position.x + 27f + disPerArrowX * (5 - i), transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
            i = fireCount % 5;
            temp = new Vector3(transform.position.x + 27f + disPerArrowX * (5 - i), transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
            i = fireCount % 5;
            temp = new Vector3(transform.position.x + 27f + disPerArrowX * (5 - i), transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
        }
        else
        {
            int fireCount = 5;
            int i = (fireMax - fireCount) % 5;
            Vector3 temp = new Vector3(transform.position.x + 27f + disPerArrowX * i, transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
            i = (fireMax - fireCount) % 5;
            temp = new Vector3(transform.position.x + 27f + disPerArrowX * i, transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
            i = (fireMax - fireCount) % 5;
            temp = new Vector3(transform.position.x + 27f + disPerArrowX * i, transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
            i = (fireMax - fireCount) % 5;
            temp = new Vector3(transform.position.x + 27f + disPerArrowX * i, transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
            i = (fireMax - fireCount) % 5;
            temp = new Vector3(transform.position.x + 27f + disPerArrowX * i, transform.position.y + disPerArrowY * i, transform.position.z);
            Instantiate(arrowPrefab, temp, transform.rotation);
            fireCount--;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canFire = true;
        }
    }
}
