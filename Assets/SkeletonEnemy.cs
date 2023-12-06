using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    private Rigidbody2D skeletonRb;
    private BoxCollider2D skeletonCollider;
    private Animator skeletonAnim;

    ItemsSpawner boost;
    private Transform deadSkeletonPos;
    [SerializeField]private Transform playerPos;

    private float skeletonSpeed = 0.4f;
    
    
    private int shieldLife = 2;
    private int skeletonLife = 3;

    
    void Start()
    {
        boost = FindObjectOfType<ItemsSpawner>();
        skeletonAnim = GetComponent<Animator>();
        skeletonCollider = GetComponent<BoxCollider2D>();
        skeletonRb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        skeletonRb.velocity = new Vector2(skeletonSpeed, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            skeletonSpeed *= -1;
        }


        if (collision.tag == "Dagger")
        {
            if (shieldLife > 0)
            {
                skeletonAnim.SetTrigger("Shield");
                shieldLife--;
            }
            if(skeletonLife> 0)
            {
                skeletonAnim.SetTrigger("Hit");
                skeletonLife--;
            }
            if(skeletonLife==0) 
            {
                skeletonAnim.SetBool("Dead", true);
                skeletonSpeed = 0;
                deadSkeletonPos = transform;
            }


        }
        if (collision.tag == "Player")
        {
           // FollowPlayer();
          
        }
    }




    private void FollowPlayer()
    {
        Vector2 direction = playerPos.position - transform.position;
        
        if (direction.magnitude > 0.2f)
        {
            direction.Normalize();
            skeletonRb.velocity = direction * skeletonSpeed;
        }
        else
        {
            skeletonRb.velocity = Vector2.zero;
        }
    }
    
    public void DestroySkeleton()
    {
        Destroy(this.gameObject);
        Debug.Log("enemys dead");

    }

    private void DisableSkeletonCollider()
    {
        skeletonCollider.enabled = false;
    }

    public void SpikeBoost()
    {
        boost.SpawnBoost(deadSkeletonPos);
    }
}
