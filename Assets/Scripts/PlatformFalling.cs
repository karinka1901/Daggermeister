using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    private Animator platformAnim;


    private void Start()
    {
        platformAnim = GetComponent<Animator>();
        gameObject.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            platformAnim.SetBool("Flicker", true);
            Invoke("DestroyPlatform", 2.5f);
            Invoke("RestorePlatform", 4);
        }
    }

    private void DestroyPlatform()
    {
        gameObject.SetActive(false);
        platformAnim.SetBool("Flicker", false);
    }

    private void RestorePlatform()
    {
        gameObject.SetActive(true);
        
    }



}
