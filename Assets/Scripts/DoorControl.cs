using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorControl : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private PlayerControl playerControl;
    void Start()
    {
        anim = GetComponent<Animator>();
        //playerControl = GetComponent<PlayerControl>();
    }

    void Update()
    {
        if(playerControl.collectedKey > 0) {
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
            if (playerControl.collectedKey > 0)
            {
                
                StartCoroutine(NewScene(0.3f));
            }
            else
            {
                Debug.Log("door is locked");
            }
        }
    }

    private IEnumerator NewScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
