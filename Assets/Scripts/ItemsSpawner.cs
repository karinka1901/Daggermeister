using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [Header("GEMS")]
    public GameObject gemPrefab;
    public Transform[] gemPos;

    [Header("BOOST")]
    public GameObject boostPrefab;

    [Header("ITEMS")]
    public GameObject itemPrefab;

    //[Header("KEY")]
    //public GameObject keyPrefab;
    //public Transform keyPos;

    private int gemsNum = 6;




    void Start()
    {
        SpawnGems();
       // SpawnKey();
        //SpawnBoost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGems()
    {
        for (int i = 0; i < gemsNum; i++)
        {
            GameObject spawnedGem = Instantiate(gemPrefab, gemPos[i].position, gemPos[i].rotation);
        }
    }

    public void SpawnBoost(Transform boostPos)
    {
    
        GameObject spawnedBoost = Instantiate(boostPrefab, boostPos.position, boostPos.rotation);
    }


    //public void SpawnKey()
    //{
    //    GameObject spawnedKey = Instantiate(keyPrefab, keyPos.position, keyPos.rotation);
    //}

    public void SpawnItem(Transform itemPos)
    {
        GameObject spawnedItem = Instantiate(itemPrefab, itemPos.position, itemPos.rotation);
    }


}
