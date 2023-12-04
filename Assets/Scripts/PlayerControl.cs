using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class PlayerControl : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    private Animator animator;

    [Header("Movement")]
    public bool isFacingRight = true;
    private float horizontalInput;
    [SerializeField] private float speed = 1f;
    public bool canMove;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private int jumpsLeft;
    [SerializeField] private int maxJumps =2;

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
    [SerializeField] private bool wallJumping;


    [Header("Collectables")]
    [SerializeField] public int collectedGems = 0;
    //[SerializeField] public int collectedKey = 0;
    [SerializeField] public int collectedItem = 0;
    public bool activeQuest;


    [Header("Underwater")]
    [SerializeField] private float newDrag = 10f;
    [SerializeField]private float newGravity = 0.5f;
    [SerializeField]private GameObject WaterHelmet;
    private bool surviveWater;



    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        WaterHelmet.SetActive(false);
        //collectedGems = 0;

        jumpsLeft = maxJumps;
        surviveWater = false;

        activeQuest = false;
        canMove = true;
    }

    private void Update()
    {

       // if (canMove)
        {
            Move();
            Jump();
            Flip();
            
            WallSlide();
            Walljump();
        }
        Animate();

        isWallTouching = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.08f, 0.37f), 0, wallLayer);

    }
    //private void FixedUpdate()
    //{
    //    Move();
    //}


    public void Flip()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }


    private void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsLeft --;
                //isJumping = true;
            }
            ///////////////////// double jump///////////////////////
            if (!isGrounded && jumpsLeft > 0 && !isSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce - 0.3f); //- 0.3f
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
            rb.velocity = new Vector2(-horizontalInput * wallJumpForce.x, wallJumpForce.y);
            jumpsLeft = maxJumps;
            Debug.Log("jumpig off the wale" + jumpsLeft);
        }
    }

    private void StopWallJump()
    {
        wallJumping = false;

    }

    private void WallSlide()
    {

        if (isWallTouching && !isGrounded && horizontalInput != 0)
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

    //private void addGem()
    //{
    //    collectedGems += 1;
    //}

    //GroundChecker
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = true;
            jumpsLeft = maxJumps;
            isJumping = false;


            animator.SetTrigger("Landing");
            animator.SetBool("isFalling", false);
        }
        //if (collision.tag == "Collectable")
        //{
            
        //    Debug.Log("collected gem: " + collectedGems);
        //    Destroy(collision.gameObject);
        //    addGem();

        //}
  
        //if (collision.tag == "Key")
        //{
        //    Destroy(collision.gameObject);
        //    collectedKey = 1;
        //}

        if (collision.tag == "Enemy" || collision.tag == "Spikes" || (collision.tag == "DeadlyLiquid" &&  !surviveWater))
        {
            canMove = false;
            animator.SetTrigger("dead");
            Debug.Log(collision.tag);
        }

        if(collision.tag == "Boost")
        {
            surviveWater = true;
            WaterHelmet.SetActive(true);
            Destroy(collision.gameObject);
        }
        if ((collision.tag =="Item"))
        {
            collectedItem = 1;
            Debug.Log("collected item");
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Water")
        {
            rb.drag = newDrag;
            rb.gravityScale *= newGravity;
            speed = 0.8f;

        }

        if(collision.tag == "Cat")
        {
            activeQuest = true;
        }
   

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = false;
        }
        if (collision.tag == "Water")
        {
            rb.drag = 0;
            rb.gravityScale = 1;
            speed = 1;
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

    }

    private void PlayerDeath()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        WaterHelmet.SetActive(false);
    }

}


