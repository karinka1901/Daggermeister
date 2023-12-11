using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour
{

    public GameObject text;
    SFXcontrol sfx;
   // AudioManager audioManager;

    void Start()
    {
        text.SetActive(true);        
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXcontrol>();
       // audioManager = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("LoadText", 2f);
    }

    public void LoadText()
    {
        text.SetActive(true);
    }
}
