using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerPos;

    void Start()
    {
        PlayerrSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayerrSpawner()
    {
        GameObject spawnedPlayer = Instantiate(playerPrefab, playerPos.position, playerPos.rotation);
    }
}
