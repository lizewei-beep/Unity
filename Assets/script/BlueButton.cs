using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueButton : MonoBehaviour
{
    PlayerBlue playerScript;
    private void Awake()
    {
        playerScript = GetComponentInParent<PlayerBlue>();
    }
    private void OnTriggerEnter2D(Collider2D collision)//检测触碰地板，与跳跃有协作
    {
        if (collision.tag == "Ground")
        {
            playerScript.canJump = 2;
            playerScript.myAnim.SetBool("Jump", false);
            playerScript.canDash = true;
        }
    }
}
