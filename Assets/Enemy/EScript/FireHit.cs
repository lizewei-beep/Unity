using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHit : MonoBehaviour
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
        Vector3 temp = new Vector3(transform.position.x, transform.position.y + 12f, transform.position.z);
        temp = new Vector3(transform.position.x + 7f, transform.position.y + 23f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x - 1f, transform.position.y + 22f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x + 15f, transform.position.y + 25f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x - 11f, transform.position.y + 24f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x + 0f, transform.position.y + 30f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x + 8f, transform.position.y + 10f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x + 2f, transform.position.y + 13f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x - 5f, transform.position.y + 14f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x - 15f, transform.position.y + 22f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
        temp = new Vector3(transform.position.x - 13f, transform.position.y + 10f, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player1")) && canFire)
        {
            StartCoroutine("Fire");
        }
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(3.0f);
        if (canFire)
        {
            Shoot();
        }
        yield return new WaitForSeconds(3.0f);
        canFire = true;
    }
}
