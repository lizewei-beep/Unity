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
    private void OnTriggerEnter2D(Collider2D collision)//��ⴥ���ذ壬����Ծ��Э��
    {
        if (collision.tag == "Ground")
        {
            playerScript.moveSpeed = 4;
            playerScript.canJump = 2;
            playerScript.myAnim.SetBool("Jump", false);
            playerScript.canDash = true;
        }
    }
}
