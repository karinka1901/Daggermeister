using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    private SkeletonEnemy skeleton;

    private void Start()
    {
        skeleton = FindObjectOfType< SkeletonEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        }
    }
}
