using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    Rigidbody2D fallingBlockRB;
    public float fallingBlockSpeed = 0.5f;
    void Start()
    {
        fallingBlockRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            fallingBlockRB.gravityScale = 1;
            //fallingBlockRB.velocity = new Vector2(0, 0);
        }
    }
}
