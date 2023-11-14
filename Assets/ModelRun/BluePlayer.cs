using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayer : MonoBehaviour
{
    public GameObject BodyCollider;
    public int canJump = 2;
    public float jumpForce = 8f;
    public float jumpDown = 4f;
    public bool isJumpPressed, canDash, isDashing;
    public Animator myAnim;
    private Vector3 originPosition;
    Rigidbody2D myRigi;

    // Start is called before the first frame update
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myRigi = GetComponent<Rigidbody2D>();
        originPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < originPosition.x) 
        {
            transform.position = Vector3.MoveTowards(transform.position, originPosition, 4 * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && canJump > 0)  
        {
            canDash = false;
            if (canJump == 2)
            {
                isJumpPressed = true;
            }
            else if (canJump == 1)
            {
                myRigi.velocity = Vector2.up * 16;
            }
            canJump--;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) && !canDash) 
        {
            myRigi.AddForce(Vector2.down * jumpDown, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) && canDash)
        {
            myAnim.SetBool("Slide",true);
            canDash = false;
            isDashing = true;
            SetColliderOff();
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            myAnim.SetBool("Slide", false);
            canDash = true;
            isDashing = false;
            SetColliderOn();
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (isJumpPressed) 
        {
            myRigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
            myAnim.SetBool("Jump", true);
        }
    }
    public void SetColliderOn()
    {
        BodyCollider.SetActive(true);
    }
    public void SetColliderOff()
    {
        BodyCollider.SetActive(false);
    }

}
