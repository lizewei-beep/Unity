using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public bool isGrounded;//是否在地面
    public Transform groundCheck;//检测点
    public LayerMask ground;//检测地面图层
    public float fallAddition = 2.5f;//下落加成
    public float jumpAddition = 3.5f;//跳跃加成
    public float playerSpeed = 5f;
    public float jumpSpeed = 8f;
    public int jumpCount = 1;//跳跃次数

    private float moveX;
    private bool facingRight = true;
    private bool moveJump;
    private bool jumpHold;//长按跳跃
    private bool isJump;//传递作用
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveJump = Input.GetKeyDown("k");
        jumpHold= Input.GetKey("k");
        if (moveJump && jumpCount > 0)  
        {
            isJump = true;
        }
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Move();
        Jump();
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
        if (!facingRight && moveX > 0)
        {
            Flip();
        }
        else if (facingRight && moveX < 0) 
        {
            Flip();
        }
    }
    private void Flip()//角色反转
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
    private void Jump()
    {
        if(isGrounded)
        {
            jumpCount = 2;
        }
        if(isJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
        }
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallAddition;
        }
        else if (rb.velocity.y > 0 && !jumpHold)  
        {
            rb.gravityScale = jumpAddition;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
}
