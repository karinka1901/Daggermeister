using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    //private int gems = 0;
    private PlayerControl playerControl;
    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        playerControl.collectedGems = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {

            playerControl.collectedGems += 1;
            Debug.Log("gems " +  playerControl.collectedGems);
            Destroy(this.gameObject);

        }


    }
    
}
