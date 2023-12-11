using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorControl : MonoBehaviour
{
    private Animator anim;
    public bool newlevel;
    GameManager gameManager;
    SFXcontrol audioManager;
    [SerializeField]private PlayerControl playerControl;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("Music").GetComponent<SFXcontrol>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if(playerControl.collectedGems == 6) {
            anim.SetBool("DoorOpen", true);
        }
        else
        {
            anim.SetBool("DoorOpen", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (playerControl.collectedGems == 6)
            {       
                Invoke("NewScene", 0.3f);
          //      audioManager.PlaySFX(audioManager.door);//////////////////////////////////SFX//////////
                //newlevel = true;
            }
            else
            {
                Debug.Log("door is locked");
            }
        }
    }


    private void NewScene()
    {

        if (gameManager.getCompletedLevels() <= SceneManager.GetActiveScene().buildIndex)   
        {
            gameManager.AddCompletedLevel();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameManager.UpdateTimer(SceneManager.GetActiveScene().buildIndex, gameManager.secretTimer); // gets the current time for specific level
       
        
        Debug.Log(SceneManager.GetActiveScene().buildIndex + "     " + gameManager.getDeathCount());
        gameManager.UpdateDeath(SceneManager.GetActiveScene().buildIndex, gameManager.getDeathCount()); //gets the current death number for the specific level
        gameManager.ResetDeathCount();
        gameManager.ResetDeathCountUI();


    }


}
