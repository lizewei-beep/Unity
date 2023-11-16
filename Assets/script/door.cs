using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class door : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;
    private CapsuleCollider2D _CapsuleCollider2D;
    void Start()
    {
        _CapsuleCollider2D  = GetComponent<CapsuleCollider2D>();
    }
    public void TriggerAction()
    {
        Debug.Log("Open Door!");
        if (!isDone)
        {
            OpenDoor();
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


    private void OpenDoor()
    {
        spriteRenderer.sprite = openSprite;
        isDone = true;
        // 破坏门的碰撞体积
        _CapsuleCollider2D.enabled = false;
    }
}
