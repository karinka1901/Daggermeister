using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCat : MonoBehaviour
{
    public GameObject catPrefab;
    public GameObject enemyPrefab;
    public Transform enemyPos;
    public Transform catPos;
    BoxCollider2D catTrigger;


    private void Start()
    {
        catTrigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spawnCat();
            catTrigger.enabled =false;
        }
    }
    public void spawnCat()
    {
        GameObject spawnedCat = Instantiate(catPrefab, catPos.position, catPos.rotation );
    }

    
}
