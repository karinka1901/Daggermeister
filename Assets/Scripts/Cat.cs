using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    Animator anim;
    SpawnCat catSpawner;
    public bool catHit;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        catSpawner = FindObjectOfType<SpawnCat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("Interact", true);
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
        }
    }
}
