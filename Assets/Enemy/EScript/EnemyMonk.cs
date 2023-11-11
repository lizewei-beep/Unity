using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonk : MonoBehaviour
{
    public Vector3 targetPosition;
    public float mySpeed;
    public GameObject attackCollider, attackCollider2, attackCollider3, attackCollider4;
    private Vector3 originPosition, turnPoint;
    public int enemyLife;
    float attackDis;
    int caseX;
    Animator myAnim;
    bool isFirstTimeIdle, isAfterBattleCheck, isAlive;
    bool canAttack1,canAttack2;
    GameObject myPlayer1, myPlayer2;
    BoxCollider2D myCollider;
    SpriteRenderer mySr;
    Rigidbody2D myRigi;
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
        canAttack1 = true;
        canAttack2 = true;
        MonkHealthBar.HealthMax = enemyLife;
        MonkHealthBar.HealthCurrent = enemyLife;
    }
    void Update()
    {
        MoveAndAttack();
        MonkHealthBar.HealthCurrent = enemyLife;
    }
    void MoveAndAttack()
    {
        if (isAlive)
        {
            if(enemyLife<15)
            {
                attackDis = 3.5f;
                caseX = 3;
            }
            else if(enemyLife<30)
            {
                attackDis = 3.1f;
                caseX = 2;
            }
            else
            {
                attackDis = 2.8f;
                caseX = 1;
            }
            if (Vector3.Distance(myPlayer1.transform.position, transform.position) < attackDis || Vector3.Distance(myPlayer2.transform.position, transform.position) < attackDis && canAttack1)
            {
                if (myPlayer1.transform.position.x <= transform.position.x && Vector3.Distance(myPlayer1.transform.position, transform.position) < attackDis || myPlayer2.transform.position.x <= transform.position.x && Vector3.Distance(myPlayer2.transform.position, transform.position) < attackDis)
                {
                    transform.localScale = new Vector3(-7.0f, 7.0f, 7.0f);
                    StartCoroutine("AttackStorm");
                }
                else
                {
                    transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
                    StartCoroutine("AttackStorm");
                }
                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle 0"))
                {
                    return;
                }
                isAfterBattleCheck = true;
                return;
            }
            else if (Vector3.Distance(myPlayer1.transform.position, transform.position) < 6f || Vector3.Distance(myPlayer2.transform.position, transform.position) < 6f && canAttack2) 
            {
                canAttack1 = false;
                if (Vector3.Distance(myPlayer1.transform.position, transform.position) < 6f && Vector3.Distance(myPlayer1.transform.position, transform.position) > 3f)
                {
                    if(myPlayer1.transform.position.x <= transform.position.x)
                    {
                        transform.localScale = new Vector3(-7.0f, 7.0f, 7.0f);
                    }
                    else
                    {
                        transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
                    }
                    Vector3 myTarget = new Vector3(myPlayer1.transform.position.x, myPlayer1.transform.position.y, myPlayer1.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, myTarget, 25.0f * Time.deltaTime);
                    StartCoroutine("Attack2");
                }
                else if (Vector3.Distance(myPlayer2.transform.position, transform.position) < 6f && Vector3.Distance(myPlayer2.transform.position, transform.position) > 3f) 
                {
                    if (myPlayer2.transform.position.x <= transform.position.x)
                    {
                        transform.localScale = new Vector3(-7.0f, 7.0f, 7.0f);
                    }
                    else
                    {
                        transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
                    }
                    Vector3 myTarget = new Vector3(myPlayer2.transform.position.x, myPlayer2.transform.position.y, myPlayer2.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, myTarget, 25.0f * Time.deltaTime);
                    StartCoroutine("Attack2");
                }

            }
            else//玩家离开怪物恢复原本方向
            {
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
    IEnumerator Attack2()
    {
        myAnim.SetTrigger("Attack2");
        yield return new WaitForSeconds(0.5f);
        canAttack2 = false;
        yield return new WaitForSeconds(1f);
        canAttack1 = true;
        yield return new WaitForSeconds(2f);
        canAttack2 = true;
    }

    IEnumerator turnRight(bool turnR)
    {
        
        if (turnR)
        {
            transform.localScale = new Vector3(-7.0f, 7.0f, 7.0f);
        }
        else
        {
            transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
        }
        yield return new WaitForSeconds(0.5f);
    }
    public void SetAttackColliderOn()
    {
        attackCollider.SetActive(true);
    }
    public void SetAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }

    public void SetAttackCollider2On()
    {
        attackCollider2.SetActive(true);
    }
    public void SetAttackCollider2Off()
    {
        attackCollider2.SetActive(false);
    }
    public void SetAttackCollider3On()
    {
        attackCollider3.SetActive(true);
    }
    public void SetAttackCollider3Off()
    {
        attackCollider3.SetActive(false);
    }
    public void SetAttackCollider4On()
    {
        attackCollider4.SetActive(true);
    }
    public void SetAttackCollider4Off()
    {
        attackCollider4.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            enemyLife--;
            if (enemyLife >= 1)
            {
                myAnim.SetTrigger("Hurt");
            }
            else if (enemyLife < 1)
            {
                isAlive = false;
                myCollider.enabled = false;
                myAnim.SetTrigger("Die");
                StartCoroutine("AfterDie");
            }
        }
    }
    IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(0.5f);
        mySr.material.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    IEnumerator AttackStorm()
    {
        yield return new WaitForSeconds(1f);
        if (caseX == 1) 
        {
            yield return new WaitForSeconds(2f);
            /*int a = Random.Range(1, 3);
            if (a == 2) 
            myAnim.SetTrigger("Attack4");
            else
                myAnim.SetTrigger("Attack");*/
            myAnim.SetTrigger("Attack3");
        }
        else if (caseX == 2)
        {
            yield return new WaitForSeconds(1.6f);
            myAnim.SetTrigger("Attack");
        }
        else
        {
            yield return new WaitForSeconds(1.3f);
            myAnim.SetTrigger("Attack4");
        }
    }
}
