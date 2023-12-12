using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    Rigidbody2D fallingBlockRB;
    public float fallingBlockSpeed = 0.5f;
    SFXcontrol audioManager;
    void Start()
    {
        fallingBlockRB = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXcontrol>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           fallingBlockRB.gravityScale = 1;
            audioManager.PlaySFX(audioManager.fallingBlock);//////////////////////////////////SFX//////////
           
            

            //fallingBlockRB.velocity = new Vector2(0, 0);
        }
    }
}
