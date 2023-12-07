using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArea : MonoBehaviour
{
    private FlyingEnemy bat;
    private PlayerControl player;

    private void Start()
    {
        bat = FindObjectOfType<FlyingEnemy>();
        player = FindObjectOfType<PlayerControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
          
                bat.batAnim.SetBool("Follow", true);
                Vector2 direction = (player.transform.position - transform.position).normalized;
                bat.batRb.velocity = new Vector2(bat.batSpeed, 0);
                bat.batRb.velocity = direction * bat.batSpeed;
    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bat.batAnim.SetBool("Follow", false);

           bat.batRb.velocity = new Vector2(0, 0);

        }
    }
}
