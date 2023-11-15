using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlue : MonoBehaviour
{
    public GameObject arrowPrefab, arrowPrefab2;
    public float moveSpeed;
    float jumpForce;
    public GameObject attackCollider;

    public Transform target;

    public int playerLife;
    [HideInInspector]
    public Animator myAnim;
    Rigidbody2D myRigi;
    SpriteRenderer mySr;

    public bool isJumpPressed, isAttack, isHurt, canBeHurt;//�Ƿ�����Ծ������
    public int canJump;

    public bool canDash = true;
    bool isDashing;
    float dashingPower = 20f;
    float dashingTime = 0.1f;
    float noDashTime = 1f;
    int maxPLife;
    int canCure;
    bool canProtect;
    int canBigFire;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myRigi = GetComponent<Rigidbody2D>();
        mySr = GetComponent<SpriteRenderer>();

        isJumpPressed = false;
        canJump = 2;
        moveSpeed = 4.0f;
        jumpForce = 16.5f;
        isAttack = false;
        isHurt = false;
        canBeHurt = true;
        playerLife = 10;
        maxPLife = playerLife;
        canCure = 2;
        canProtect = true;
        HealthBar1.HealthMax = playerLife;
        HealthBar1.HealthCurrent = playerLife;
        canBigFire = 3;
    }


    void Update()
    {

        HealthBar1.HealthCurrent = playerLife;
        
        if (isDashing)
        {
            return;
        }
        if (myRigi.velocity.y < 0)
        {
            myRigi.gravityScale = 7;
        }
        else
        {
            myRigi.gravityScale = 4;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && canJump>0 && !isHurt)       //��Ծ��⣬�ڶ�����ͬ
        {
            moveSpeed = 8 ;
            moveSpeed = 10;
            canDash = false;
            if (canJump == 2)
            {
                isJumpPressed = true;
            }
            else if (canJump == 1)
            {
                myRigi.velocity = Vector2.up * moveSpeed * 2;
                myRigi.velocity = Vector2.up * moveSpeed * 4;
                myRigi.velocity = Vector2.up * 16;
            }
            canJump--;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) && !isHurt)//��ͬ
        {
            canBigFire++;
            myAnim.SetTrigger("Attack");
            isAttack = true;
            canProtect = true;
            //canJump = false;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) && canDash)
        {
            myAnim.SetTrigger("Slide");
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) && !isHurt && canBigFire > 2) 
        {
            canBigFire = 0;
            myAnim.SetTrigger("Power");
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) && !isHurt && canCure > 0)
        {
            myAnim.SetTrigger("Cure");
            canCure--;
            playerLife = maxPLife < playerLife + 3 ? maxPLife : playerLife + 3;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isHurt && canProtect)
        {
            canProtect = false;
            canBeHurt = false;
            myAnim.SetBool("Protect", true);
            StartCoroutine("ProHurt");
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            myAnim.SetBool("Protect", false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) && !isHurt && canBigFire > 1)
        {
            canBigFire =0;
            myAnim.SetTrigger("Sword");
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Vector3 targetPos = target.position;
        Vector2 position = myRigi.position;//��¼��ʼλ��
        bool rtemp = true;
        bool ltemp = true;
        if (position.x - targetPos.x > 30)
        {
            rtemp = false;
        }
        else if (targetPos.x - position.x > 30)
        {
            ltemp = false;
        }
        else
        {
            rtemp = true;
            ltemp = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && ltemp) //�����ƶ�����������ͬ
        {
            myAnim.SetBool("Run", true);
            if (moveSpeed > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);//ת��
            }
            position.x -= moveSpeed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && rtemp) //�����ƶ������Ĵ���ͬ
        {
            myAnim.SetBool("Run", true);
            if (moveSpeed > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            position.x += moveSpeed * Time.fixedDeltaTime;
        }
        else//���ƶ��������ܲ�����
        {
            myAnim.SetBool("Run", false);
        }
        if (isJumpPressed)  //��ʼ��Ծ
        {
            myRigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
            myAnim.SetBool("Jump", true);
        }
        if (!isHurt)
        {
            myRigi.position = position;//����λ��
        }
    }
    IEnumerator ProHurt()
    {
        yield return new WaitForSeconds(2.0f);
        canBeHurt = true;
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Water"))
        {
            playerLife = 0;
            isHurt = true;
            moveSpeed = 0;
            myAnim.SetBool("Die", true);
            StartCoroutine("AfterDie");
        }
        if ((collision.tag == "Enemy" || collision.CompareTag("Trap")) && !isHurt && canBeHurt)
        {
            playerLife--;
            HealthBar1.HealthCurrent = playerLife;
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
                StartCoroutine("AfterDie");
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
        if ((collision.tag == "Water"))
        {
            playerLife = 0;
            isHurt = true;
            moveSpeed = 0;
            myAnim.SetBool("Die", true);
            StartCoroutine("AfterDie");
        }
        if ((collision.tag == "Enemy" || collision.CompareTag("Trap")) && !isHurt && canBeHurt)
        {
            playerLife--;
            HealthBar1.HealthCurrent = playerLife;
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
                StartCoroutine("AfterDie");
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
    IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(1.0f);
        mySr.material.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        canBeHurt = false;
        float dashingGravity = myRigi.gravityScale;
        myRigi.gravityScale = 0f;
        myRigi.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        myRigi.gravityScale = dashingGravity;
        isDashing = false;
        Vector3 targetPos = target.position;
        Vector2 position = myRigi.position;//��¼��ʼλ��
        if (position.x - targetPos.x > 30 || targetPos.x - position.x > 30 || targetPos.y - position.y > 10 || position.y - targetPos.y > 10) 
        {
            myRigi.position = new Vector2(targetPos.x, targetPos.y);
        }
        yield return new WaitForSeconds(noDashTime);
        canDash = true;
        canBeHurt = true;
    }
    public void SetFireOn()
    {
        Vector3 temp = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
        temp = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
        Instantiate(arrowPrefab, temp, transform.rotation);
    }
    public void SetSwordOn()
    {
        Vector3 temp = new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z);
        Instantiate(arrowPrefab2, temp, transform.rotation);
    }
}
