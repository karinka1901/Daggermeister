using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    //private int gems = 0;
    private PlayerControl playerControl;
    SFXcontrol audioManager;
    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        audioManager =GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXcontrol>();
       // playerControl.collectedGems = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {

            playerControl.collectedGems += 1;
            Debug.Log("gems " +  playerControl.collectedGems);
            audioManager.PlaySFX(audioManager.collectedGem);//////////////////////////////////SFX//////////
            Destroy(this.gameObject);

        }




    }

}
