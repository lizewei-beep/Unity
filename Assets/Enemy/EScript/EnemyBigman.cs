using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigman : MonoBehaviour
{
    public Vector3 targetPosition;
    public float attentionDistance;
    public float mySpeed;
    public GameObject attackCollider, attackCollider2;
    private Vector3 originPosition, turnPoint;
    public int enemyLife;
    Animator myAnim;
    bool isFirstTimeIdle, isAfterBattleCheck, isAlive, canBeHurt;
    GameObject myPlayer1, myPlayer2;
    BoxCollider2D myCollider;
    SpriteRenderer mySr;
    Rigidbody2D myRigi;
    int tempCD;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        myRigi = GetComponent<Rigidbody2D>();
        mySr = GetComponent<SpriteRenderer>();
        myPlayer1 = GameObject.Find("PlayerBlue");
        myPlayer2 = GameObject.Find("PlayerRed");
        originPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        isFirstTimeIdle = true;
        isAfterBattleCheck = false;
        isAlive = true;
        canBeHurt = true;
        EhealthBar.HealthMax = enemyLife;
        EhealthBar.HealthCurrent = enemyLife;
        tempCD = 0;
    }
    void Update()
    {
        MoveAndAttack();
    }
    void MoveAndAttack()
    {
        if (isAlive)
        {
            if (Vector3.Distance(myPlayer1.transform.position, transform.position) < attentionDistance || Vector3.Distance(myPlayer2.transform.position, transform.position) < attentionDistance)
            {
                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle 0")|| myAnim.GetCurrentAnimatorStateInfo(0).IsName("Cast") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Spell"))
                {
                    return;
                }
                if (myPlayer1.transform.position.x <= transform.position.x && Vector3.Distance(myPlayer1.transform.position, transform.position) < attentionDistance || myPlayer2.transform.position.x <= transform.position.x && Vector3.Distance(myPlayer2.transform.position, transform.position) < attentionDistance)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    StartCoroutine("AttackStorm");
                }
                else
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    StartCoroutine("AttackStorm");
                }

                //myAnim.SetTrigger("Attack");
                isAfterBattleCheck = true;
                return;
            }
            else if (Vector3.Distance(myPlayer1.transform.position, transform.position) < attentionDistance + 6f && Vector3.Distance(myPlayer1.transform.position, transform.position) > attentionDistance || Vector3.Distance(myPlayer2.transform.position, transform.position) < attentionDistance + 6f && Vector3.Distance(myPlayer2.transform.position, transform.position) > attentionDistance)
            {
                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle 0") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Cast") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Spell"))
                {
                    return;
                }
                mySpeed = 7;
                if(myPlayer1.transform.position.x <= transform.position.x || myPlayer2.transform.position.x <= transform.position.x)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    turnPoint = targetPosition;
                }
                else
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    turnPoint = originPosition;
                }
            }
            else//玩家离开怪物恢复原本方向
            {
                mySpeed = 3;
                if (isAfterBattleCheck)
                {
                    if (turnPoint == targetPosition)
                    {
                        StartCoroutine(turnRight(false));
                    }
                    else if (turnPoint == originPosition)
                    {
                        StartCoroutine(turnRight(true));
                    }
                    isAfterBattleCheck = false;
                }
            }
            if (transform.position.x == targetPosition.x)
            {
                myAnim.SetTrigger("Idle");
                turnPoint = originPosition;
                StartCoroutine(turnRight(true));
                isFirstTimeIdle = false;
            }
            else if (transform.position.x == originPosition.x)
            {
                if (!isFirstTimeIdle)
                {
                    myAnim.SetTrigger("Idle");
                }
                turnPoint = targetPosition;
                StartCoroutine(turnRight(false));
            }
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                transform.position = Vector3.MoveTowards(transform.position, turnPoint, mySpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator turnRight(bool turnR)
    {
        yield return new WaitForSeconds(1.0f);
        if (turnR)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
    public void SetAttackColliderOn()
    {
        attackCollider.SetActive(true);
    }
    public void SetAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }

    public void NoHurtPosibleOn()
    {
        canBeHurt = false;
    }
    public void NoHurtPosibleOff()
    {
        canBeHurt = true;
    }
    public void SetAttackCollider2On()
    {
        attackCollider2.SetActive(true);
    }
    public void SetAttackCollider2Off()
    {
        attackCollider2.SetActive(false);
    }

    IEnumerator SetIsHurtFalse()
    {
        yield return new WaitForSeconds(1.0f);
        canBeHurt = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack" && canBeHurt) 
        {
            enemyLife--;
            EhealthBar.HealthCurrent = enemyLife;
            if (enemyLife >= 1)
            {
                canBeHurt = false;
                myAnim.SetTrigger("Hurt");
                StartCoroutine("SetIsHurtFalse");
            }
            else if (enemyLife < 1)
            {
                isAlive = false;
                myCollider.enabled = false;
                myAnim.SetTrigger("Die");
                DoorToCave.canOpen = true;
                StartCoroutine("AfterDie");
            }
        }
    }
    IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(1.0f);
        mySr.material.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
    IEnumerator AttackStorm()
    {
        tempCD++;
        if (tempCD % 2 == 1) 
        {
            myAnim.SetTrigger("Attack");
        }
        else
        {
            myAnim.SetTrigger("Attack2");
        }
        if (enemyLife < 20) 
        {
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }
    }
}
