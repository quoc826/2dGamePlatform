using System;
using Unity.Mathematics;
using UnityEngine;

public class guyPlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 5f;

    public float jumpForce = 10f;
    public Boolean isFacingRight = true;

    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask IsGround;

    public Animator anim;
    public int IsRun;
    public int IsJump;





   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        IsRun = Animator.StringToHash("isRun");
        IsJump = Animator.StringToHash("isJump");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        jump();
    }

    public void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        
        if(math.abs(rb.linearVelocity.x ) > 0.1f && isGrounded())
        {
            anim.SetBool(IsRun, true);
        }
        else
        {
            anim.SetBool(IsRun, false);
        }


        // flip
        if (horizontal > 0.1f && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < -0.1f && isFacingRight)
        {
            Flip();
        }
    }
    

    public bool FacingRight()
    {
        return isFacingRight;
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

 
    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, IsGround);
    }
    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger(IsJump);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
