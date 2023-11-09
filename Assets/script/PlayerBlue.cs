using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlue : MonoBehaviour
{

    float moveSpeed;
    float jumpForce;
    public GameObject attackCollider;

    public Transform target;

    int playerLife;
    [HideInInspector]
    public Animator myAnim;
    Rigidbody2D myRigi;
    SpriteRenderer mySr;

    public bool isJumpPressed, canJump, isAttack, isHurt, canBeHurt;//是否在跳跃过程中


    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myRigi = GetComponent<Rigidbody2D>();
        mySr = GetComponent<SpriteRenderer>();

        isJumpPressed = false;
        canJump = true;
        moveSpeed = 4.0f;
        jumpForce = 22.0f;
        isAttack = false;
        isHurt = false;
        canBeHurt = true;

        playerLife = 3;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2) && canJump && !isHurt)       //跳跃检测，第二处不同
        {
            isJumpPressed = true;
            canJump = false;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) && !isHurt)//不同
        {
            myAnim.SetTrigger("Attack");
            isAttack = true;
            //canJump = false;
        }
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = target.position;
        Vector2 position = myRigi.position;//记录初始位置
        if (isAttack) 
        {
            moveSpeed = 0;
        }
        bool rtemp = true;
        bool ltemp = true;
        if (position.x - targetPos.x > 20)
        {
            rtemp = false;
        }
        else if (targetPos.x - position.x > 20)
        {
            ltemp = false;
        }
        else
        {
            rtemp = true;
            ltemp = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && ltemp) //向左移动，第三处不同
        {
            myAnim.SetBool("Run", true);
            if (moveSpeed > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);//转向
            }
            position.x -= moveSpeed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && rtemp) //向右移动，第四处不同
        {
            myAnim.SetBool("Run", true);
            if (moveSpeed > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            position.x += moveSpeed * Time.fixedDeltaTime;
        }
        else//不移动不播放跑步动画
        {
            myAnim.SetBool("Run", false);
        }
        if (isJumpPressed)  //开始跳跃
        {
            myRigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
            myAnim.SetBool("Jump", true);
        }
        if (!isHurt)
        {
            myRigi.position = position;//更新位置
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !isHurt && canBeHurt)  
        {
            playerLife--;
            if (playerLife >= 1)
            {
                isHurt = true;
                canBeHurt = false;
                mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
                myAnim.SetBool("Hurt", true);
                if (transform.localScale.x == 1.0f)
                {
                    myRigi.velocity = new Vector2(-2.0f, 8.0f);
                }
                else if (transform.localScale.x == -1.0f)
                {
                    myRigi.velocity = new Vector2(2.0f, 8.0f);
                }
                StartCoroutine("SetIsHurtFalse");
            }
            else if (playerLife < 1)
            {
                isHurt = true;
                moveSpeed = 0;
                myAnim.SetBool("Die", true);
            }
        }
    }
    IEnumerator SetIsHurtFalse()
    {
        yield return new WaitForSeconds(1.0f);
        isHurt = false;
        myAnim.SetBool("Hurt", false);
        yield return new WaitForSeconds(1.0f);
        canBeHurt = true;
        mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 1.0f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !isHurt && canBeHurt)
        {
            playerLife--;
            if (playerLife >= 1)
            {
                isHurt = true;
                canBeHurt = false;
                mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
                myAnim.SetBool("Hurt", true);
                if (transform.localScale.x == 1.0f)
                {
                    myRigi.velocity = new Vector2(-2.0f, 8.0f);
                }
                else if (transform.localScale.x == -1.0f)
                {
                    myRigi.velocity = new Vector2(2.0f, 8.0f);
                }
                StartCoroutine("SetIsHurtFalse");
            }
            else if (playerLife < 1)
            {
                isHurt = true;
                moveSpeed = 0;
                myAnim.SetBool("Die", true);
            }
        }
    }
    public void SetIsAttackFalse()
    {
        moveSpeed = 4.0f;
        isAttack = false;
        //canJump = true;
        myAnim.ResetTrigger("Attack");
    }

    public void ForIsHurtSetting()
    {
        moveSpeed = 4.0f;
        isAttack = false;
        myAnim.ResetTrigger("Attack");
        attackCollider.SetActive(false);
    }
    public void SetAttackColliderOn()
    {
        attackCollider.SetActive(true);
    }
    public void SetAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }
}
