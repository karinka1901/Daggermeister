using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private bool isFacingRight = true;
    private float horizontalDirection;
    [SerializeField] private float speed = 1f;
    [Header("Jump")]

    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private int jumpsLeft;
    [SerializeField] private int maxJumps = 1;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;

    [Header("Wall Slide")]
    [SerializeField] LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    bool isWallTouching;
    bool isSliding;
    [SerializeField] private float wallSlidingSpeed;

    [Header("Wall Jump")]
    [SerializeField] Vector2 wallJumpForce;
    [SerializeField] private float wallJumpDuration;
    private bool wallJumping;






    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        jumpsLeft = maxJumps;
    }

    private void Update()
    {
        Move();
        Jump();
        Flip();
        Animate();
        WallSlide();
        Walljump();

       

        isWallTouching = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.08f, 0.37f), 0, wallLayer);
    }



    private void Move()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);

    }


    private void Jump()
    {
        if (isGrounded)
        {
            jumpsLeft = maxJumps;
            isJumping = false;

        }


        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsLeft--;
            }

            if (!isGrounded && jumpsLeft > 0 ) // double jump
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce - 0.5f);
                jumpsLeft--;
                Debug.Log("second jump");
            }
            if (isSliding)
            {
                wallJumping = true;
                Invoke("StopWallJump", wallJumpDuration);
                jumpsLeft = maxJumps;
                Debug.Log("im slifdin");
            }

        }
    }
    private void Walljump()
    {
        if (wallJumping)
        {

            rb.velocity = new Vector2(-horizontalDirection * wallJumpForce.x, wallJumpForce.y);
            jumpsLeft = maxJumps + 1;
            Debug.Log("am i here");
        }

        else
        {
            rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);

        }
    }

    private void StopWallJump()
    {
        wallJumping = false;

    }

    private void WallSlide()
    {

        if (isWallTouching && !isGrounded && horizontalDirection != 0)
        {
            isSliding = true;

        }
        else
        {
            isSliding = false;
        }

        if (isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    //GroundChecker
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = true;


            animator.SetTrigger("Landing");
            animator.SetBool("isFalling", false);


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = false;
        }
    }


    private void Flip()
    {
        if (isFacingRight && horizontalDirection < 0f || !isFacingRight && horizontalDirection > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }



    private void Animate()
    {
        float ySpeed = rb.velocity.y;
        float xSpeed = Mathf.Abs(rb.velocity.x);


        animator.SetFloat("xSpeed", xSpeed);
        animator.SetFloat("ySpeed", ySpeed);

        if (rb.velocity.x == 0f && rb.velocity.y == 0f)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        if (rb.velocity.y < 0f)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        animator.SetBool("isJumping", isJumping);

        if (isSliding)
        {
            animator.SetBool("isSliding", true);
        }
        else
        {
            animator.SetBool("isSliding", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("throw");
        }
    



    }

}


