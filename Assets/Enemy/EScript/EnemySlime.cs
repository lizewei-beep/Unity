using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    public Vector3 targetPosition;
    public float mySpeed;
    public GameObject attackCollider;
    private Vector3 originPosition, turnPoint;
    Animator myAnim;
    bool isFirstTimeIdle, isAfterBattleCheck;
    GameObject myPlayer1, myPlayer2;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myPlayer1 = GameObject.Find("PlayerBlue");
        myPlayer2 = GameObject.Find("PlayerRed");
        originPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        isFirstTimeIdle = true;
        isAfterBattleCheck = false;
    }
    void Update()
    {
        if (Vector3.Distance(myPlayer1.transform.position, transform.position) < 2.15f || Vector3.Distance(myPlayer2.transform.position, transform.position) < 2.15f) 
        {
            if (myPlayer1.transform.position.x <= transform.position.x && Vector3.Distance(myPlayer1.transform.position, transform.position) < 2.15f || myPlayer2.transform.position.x <= transform.position.x && Vector3.Distance(myPlayer2.transform.position, transform.position) < 2.15f) 
            {
                transform.localScale = new Vector3(-7.0f, 7.0f, 7.0f);
            }
            else
            {
                transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
            }
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle 0")) 
            {
                return;
            }
            myAnim.SetTrigger("Attack");
            isAfterBattleCheck = true;
            return;
        }
        else//玩家离开怪物恢复原本方向
        {
            if(isAfterBattleCheck)
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
            if(!isFirstTimeIdle)
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
    IEnumerator turnRight(bool turnR)
    {
        yield return new WaitForSeconds(2.0f);
        if(turnR)
        {
            transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
        }
        else
        {
            transform.localScale = new Vector3(-7.0f, 7.0f, 7.0f);
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
}
