using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public Vector3 targetPosition;
    public float mySpeed;
    public GameObject attackCollider, attackCollider2;
    private Vector3 originPosition, turnPoint;
    public int enemyLife;
    Animator myAnim;
    bool isFirstTimeIdle, isAfterBattleCheck, isAlive;
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
        BossHealthBar.HealthMax = enemyLife;
        BossHealthBar.HealthCurrent = enemyLife;
    }
    void Update()
    {
        MoveAndAttack();
        BossHealthBar.HealthCurrent = enemyLife;
    }
    void MoveAndAttack()
    {

        int temp = 1;
        if (enemyLife > 20) 
        {
            temp = Random.Range(1, 4);
        }
        else
        {
            temp = Random.Range(1, 3);
        }
        if (isAlive)
        {
            if (Vector3.Distance(myPlayer1.transform.position, transform.position) < 5f || Vector3.Distance(myPlayer2.transform.position, transform.position) < 5f)
            {
                if (myPlayer1.transform.position.x <= transform.position.x && Vector3.Distance(myPlayer1.transform.position, transform.position) < 5f || myPlayer2.transform.position.x <= transform.position.x && Vector3.Distance(myPlayer2.transform.position, transform.position) < 5f)
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    if (temp == 1) 
                    {
                        StartCoroutine("AttackStorm2");
                    }
                    else
                    {
                        StartCoroutine("AttackStorm");
                    }
                }
                else
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    if (temp == 1)
                    {
                        StartCoroutine("AttackStorm2");
                    }
                    else
                    {
                        StartCoroutine("AttackStorm");
                    }
                }
                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle 0"))
                {
                    return;
                }
                isAfterBattleCheck = true;
                return;
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
    public void SetAttackCollider2On()
    {
        attackCollider2.SetActive(true);
    }
    public void SetAttackCollider2Off()
    {
        attackCollider2.SetActive(false);
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
        yield return new WaitForSeconds(0.1f);
        mySr.material.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    IEnumerator AttackStorm()
    {
        yield return new WaitForSeconds(1f);
        myAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
    }
    IEnumerator AttackStorm2()
    {
        yield return new WaitForSeconds(0.5f);
        myAnim.SetTrigger("Attack2");
        yield return new WaitForSeconds(0.5f);
    }
}
