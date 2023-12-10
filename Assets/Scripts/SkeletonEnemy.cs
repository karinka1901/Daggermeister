using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    private Rigidbody2D skeletonRb;
    private BoxCollider2D skeletonCollider;
    private Animator skeletonAnim;
    private PlayerControl player;

    [SerializeField] private GameObject Shield;
    [SerializeField] private GameObject Sword;

    ItemsSpawner boost;
    private Transform deadSkeletonPos;
    //[SerializeField] private Transform playerPos;

    [SerializeField]private float skeletonSpeed = 0.4f;

    [SerializeField]private bool isShielded;
    
    private int shieldLife = 2;
    private int skeletonLife = 3;


    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Transform attackOrigin;
    public float frontRay =2.0f; 
    public float backRay = 1f;
    public float attackRay = 0.5f;
    private bool isAttacking;

    void Start()
    {
        boost = FindObjectOfType<ItemsSpawner>();
        skeletonAnim = GetComponent<Animator>();
        skeletonCollider = GetComponent<BoxCollider2D>();
        skeletonRb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerControl>();

        Sword.SetActive(false);

    }


    void Update()
    {
        if (!player.pauseOn)
        {
            FollowandAttack();

            //skeletonRb.velocity = new Vector2(skeletonSpeed, 0);


            if (shieldLife > 0)
            {
                isShielded = true;
            }
            if (shieldLife <= 0)
            {
                isShielded = false;
                Shield.SetActive(false);
            }
        }
        else
        {
            skeletonRb.velocity = new Vector2(0, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            FlipEnemy();
            
        }

        if (collision.tag == "Dagger")
        {
            if (shieldLife > 0)
            {
                if (!player.isFacingRight && skeletonSpeed < 0 || player.isFacingRight && skeletonSpeed > 0)
                {
                    FlipEnemy();
           
                }
           
                skeletonAnim.SetTrigger("Shield");
                shieldLife--;
            }

            if (skeletonLife > 0 && !isShielded)
            {
                skeletonAnim.SetTrigger("Hit");
                skeletonLife--;
            }

            if (skeletonLife == 0)
            {
                //skeletonAnim.SetBool("Dead", true);
                skeletonAnim.SetTrigger("dead");
                skeletonSpeed = 0;
                deadSkeletonPos = transform;
                player.activated = true;
            }

        }
    }


    private void FlipEnemy()
    {
       
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            skeletonSpeed *= -1;
        
    }



    private void FollowandAttack()
    {
        RaycastHit2D hitFront = Physics2D.Raycast(raycastOrigin.position, Vector2.right * transform.localScale.x, frontRay, LayerMask.GetMask("Player"));
        RaycastHit2D hitBack = Physics2D.Raycast(raycastOrigin.position, Vector2.left * transform.localScale.x, backRay, LayerMask.GetMask("Player"));
        RaycastHit2D hitAttack = Physics2D.Raycast(attackOrigin.position, Vector2.right * transform.localScale.x, attackRay, LayerMask.GetMask("Player"));
        Debug.DrawRay(raycastOrigin.position, Vector2.right * transform.localScale.x * frontRay, Color.red);
        Debug.DrawRay(raycastOrigin.position, Vector2.left * transform.localScale.x * backRay, Color.blue);
        Debug.DrawRay(attackOrigin.position, Vector2.right * transform.localScale.x * attackRay, Color.green);


        // Check if raycast hits the player in front
        if (hitFront.collider != null  && !isAttacking)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            skeletonRb.velocity = direction * skeletonSpeed;
        }
        if (hitBack.collider != null && !isAttacking)
        {
           FlipEnemy();
            Vector2 direction = (player.transform.position - transform.position).normalized;
            skeletonRb.velocity = direction * skeletonSpeed;
        }
        if (hitAttack.collider != null)
        {

            skeletonRb.velocity = Vector2.zero;
            skeletonAnim.SetTrigger("Attack");
            isAttacking = true;
        
        }
        else
        {
            skeletonRb.velocity = new Vector2(skeletonSpeed, 0);
        }

    }





    public void DestroySkeleton()
    {
        Destroy(this.gameObject);
    }

    private void DisableSkeletonCollider()
    {
        skeletonCollider.enabled = false;
    }

    public void SpikeBoost()
    {
        boost.SpawnBoost(deadSkeletonPos);
    }

    public void StopMovement()
    {
       // skeletonSpeed = 0;
    }
    public void ResumeMoveemnt()
    {
      

    }

    public void AttackCollider() 
    {
        Sword.SetActive(true);
    }
    public void DisableAttackCollider()
    {
        Sword.SetActive(false);
        isAttacking = false;
    }
}
