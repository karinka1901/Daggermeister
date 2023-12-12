using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Elevator elevator;
    private GameObject leverUp;
    private GameObject leverDown;
    private GameObject blockCrate;
    SFXcontrol sfx;


    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXcontrol>();

        leverUp = transform.Find("Up").gameObject;
        leverDown = transform.Find("Down").gameObject;
        blockCrate = transform.Find("Block").gameObject;

        leverDown.SetActive(false);
        leverUp.SetActive(true);
        blockCrate.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            sfx.PlaySFX(sfx.lever);//////////////////////////////////SFX//////////
            elevator.elevatorOn = true;
            leverUp.SetActive(false);
            leverDown.SetActive(true);
            blockCrate.SetActive(false);

        }
    }
}
