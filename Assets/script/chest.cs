using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class chest : MonoBehaviour,IInteractable
{
    public GameObject coin;
    public GameObject door;
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;
    public void TriggerAction()
    {
        Debug.Log("Open Chest!");
        if(!isDone)
        {
     OpenChest();
        }
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;

    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OpenChest()
    {
        spriteRenderer.sprite = openSprite;
        isDone = true;
        Instantiate(coin, transform.position, Quaternion.identity);
        door.tag = "Interactable";
    }
}
