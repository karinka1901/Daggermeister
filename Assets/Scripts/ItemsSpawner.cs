using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    public GameObject gemPrefab;
    public Transform[] gemPos;

    public GameObject boostPrefab;
    public Transform boostPos;

    public GameObject keyPrefab;
    public Transform keyPos;

    private int gemsNum = 5;




    void Start()
    {
        SpawnGems();
        SpawnKey();
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

    public void SpawnBoost()
    {
    
        GameObject spawnedBoost = Instantiate(boostPrefab, boostPos.position, boostPos.rotation);
    }


    public void SpawnKey()
    {
        GameObject spawnedKey = Instantiate(keyPrefab, keyPos.position, keyPos.rotation);
    }
    //if player respawns, respawn all gems
}
