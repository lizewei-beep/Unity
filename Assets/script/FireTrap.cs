using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public GameObject arrowPrefab;
    bool canFire;
    // Start is called before the first frame update
    private void Awake()
    {
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Shoot()
    {
        canFire = false;
        Vector3 temp = new Vector3(transform.position.x + 0f, transform.position.y + 10f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x + 5f, transform.position.y + 15f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x - 0f, transform.position.y + 20f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player1")) && canFire)
        {
            Shoot();
            
        }
    }
}
