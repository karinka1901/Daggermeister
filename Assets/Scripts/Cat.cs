using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    Animator anim;
    SpawnCat catSpawner;
    public bool catHit;
    [SerializeField]private GameObject speechBubble;
    private GameObject rewardItem;
    [SerializeField] private PlayerControl playerControl;
    private Rigidbody2D rbCat;
    private float catSpeed = 0f;


    private void Start()
    {
        anim = GetComponent<Animator>();
        catSpawner = FindObjectOfType<SpawnCat>();
        rbCat =GetComponent<Rigidbody2D>();
        rewardItem = GameObject.FindGameObjectWithTag("Key");
        speechBubble.SetActive(false);
        rewardItem.SetActive(false);

    }

    private void Update()
    {
        rbCat.velocity = new Vector2(catSpeed, rbCat.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("Interact", true);

            if (playerControl.collectedItem <= 0)
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
            Destroy(this.gameObject);
            GameObject enemy = Instantiate(catSpawner.enemyPrefab, catSpawner.enemyPos.position, catSpawner.enemyPos.rotation);
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
