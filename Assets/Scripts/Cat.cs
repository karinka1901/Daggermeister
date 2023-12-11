using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    Animator anim;
    
    SpawnCat catSpawner;
    public bool catHit;
    [SerializeField]public GameObject speechBubble;
    private GameObject rewardItem;
    [SerializeField] private PlayerControl playerControl;
    private Rigidbody2D rbCat;
    private float catSpeed = 0f;
    SFXcontrol audioManager;

    private Transform deadCatPos;
    public GameObject enemyPrefab;


    private void Start()
    {
        anim = GetComponent<Animator>();
        catSpawner = FindObjectOfType<SpawnCat>();
        rbCat =GetComponent<Rigidbody2D>();
        rewardItem = GameObject.FindGameObjectWithTag("Key");
        speechBubble.SetActive(false);
        rewardItem.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXcontrol>();

    }

    private void Update()
    {
        rbCat.velocity = new Vector2(catSpeed, rbCat.velocity.y);
        //speechBubble.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("Interact", true);
            audioManager.PlaySFX(audioManager.cat);//////////////////////////////////SFX//////////

            if (playerControl.collectedItem == 0)
            {
                speechBubble.SetActive(true);
                Debug.Log("no fish");
            }
            if (playerControl.collectedItem == 1)
            {
                speechBubble.SetActive(false);
                rewardItem.SetActive(true);
                anim.SetTrigger("Run");
                
            }
        }

        if (collision.tag == "Dagger")
        {
           playerControl.isDead = true;
            deadCatPos = transform;
            Destroy(this.gameObject);
            Instantiate(enemyPrefab, deadCatPos.position, deadCatPos.rotation);
            catHit = true;


        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("Interact", false);
            speechBubble.SetActive(false);
        }
    }

    public void catStartRun()
    {
        catSpeed = 0.4f;
        
    }
    public void catStopRun()
    {
        gameObject.SetActive(false);
    }
}
