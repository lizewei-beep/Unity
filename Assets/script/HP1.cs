using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HP1 : MonoBehaviour
{
    public GameObject dialogBox;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;

            // 回复碰到的玩家血量
            if(other.gameObject.GetComponent<PlayerBlue>().playerLife < 9)
                other.gameObject.GetComponent<PlayerBlue>().playerLife += 1;
            else
                other.gameObject.GetComponent<PlayerBlue>().playerLife = 10;
        }
        if (other.gameObject.tag == "Player1")
        {
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;

            // 回复碰到的玩家血量
            if (other.gameObject.GetComponent<PlayerRed>().playerLife < 9)
                other.gameObject.GetComponent<PlayerRed>().playerLife += 1;
            else
                other.gameObject.GetComponent<PlayerRed>().playerLife =10;

        }
    }
}
