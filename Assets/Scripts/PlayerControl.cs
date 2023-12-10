using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class PlayerControl : MonoBehaviour
{
    private GameManager gameManager;
    //public int playerDeathCounter = 0;
    public bool godMode;
    public bool activated;
    public bool pauseOn;
    public ParticleSystem skull;
    public int unlockedLevels = 0;



    
    [Header("Components")]
    public Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D playerBox;

    [Header("Movement")]
    public bool isFacingRight = true;
    private float horizontalInput;
    [SerializeField] private float speed = 1f;
    private bool isDead;


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
    [SerializeField] public int collectedItem = 0;
    public bool activeQuest;
    [SerializeField]private bool antiSpikeOn;


    [Header("Underwater")]
    [SerializeField] private float newDrag = 10f;
    [SerializeField]private float newGravity = 0.5f;
    [SerializeField]private GameObject WaterHelmet;
    [SerializeField]private bool surviveWater;



    private void Start()
    {
        playerBox = GetComponent<CapsuleCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        WaterHelmet.SetActive(false);

        isDead = false;



        jumpsLeft = maxJumps;
        surviveWater = false;

        activeQuest = false;
        

        antiSpikeOn = false;
        collectedGems = 0;
        

        gameManager.pauseOn = false;
        
    }

    private void Update()
    {
        if (!gameManager.pauseOn)
        {

            /////////////////CHEATS//////////////////////////////
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                godMode = !godMode;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                collectedGems = 6;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                WaterHelmet.SetActive(!WaterHelmet.activeSelf);
                surviveWater = !surviveWater;
            }

            Animate();

            if (!isDead)
            {
                Move();
                Jump();
                Flip();

                WallSlide();
                Walljump();

                isWallTouching = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.08f, 0.37f), 0, wallLayer);
            }
            else
            {
                
              // Invoke("PlayerDeath", 1f);
            }
        }

        else
        {
            Debug.Log("InMenu");
        }
        


    }


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

        if (collision.tag == "Enemy" || (collision.tag == "Spikes" && !antiSpikeOn) || (collision.tag == "DeadlyLiquid" &&  !surviveWater))
        {
            if (!godMode)
            {
               
                animator.SetTrigger("dead");
                isDead = true;
                
                
                Debug.Log(collision.tag);
                
            }
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

        //Collectibles 

        if (collision.tag == "Boost")
        {
            surviveWater = true;
            WaterHelmet.SetActive(true);
            Destroy(collision.gameObject);
        }
        if ((collision.tag == "Item"))
        {
            collectedItem = 1;
            Debug.Log("collected item");
            Destroy(collision.gameObject);
        }

        if (collision.tag == "AntiSpike")
        {
            antiSpikeOn = true;
            Debug.Log(antiSpikeOn);
            Destroy(collision.gameObject);
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
        playerBox.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        gameManager.AddDeathCount();
        gameManager.AddDeathCountUI();
        
        WaterHelmet.SetActive(false);
        Debug.Log(gameManager.getDeathCount());
        Invoke("ReloadAfterDeath", 0.5f);
        
    }
    private void ReloadAfterDeath()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }




    public void DeathScreen()
    {
        skull.Play();
    }

    public void Dead()
    {
        isDead = true;
    }

}


