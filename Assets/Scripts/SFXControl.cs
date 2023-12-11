using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXcontrol : MonoBehaviour
{
    [Header("SFX CLIPS")]
    [Header("Player Sounds")]

    public AudioClip walk;
    public AudioClip jump;
    public AudioClip fall;
    public AudioClip wallJump;
    public AudioClip sliding;
    public AudioClip death;
    public AudioClip collectedGem;

    [Header("Dagger")]
    public AudioClip daggerThrow;
    public AudioClip daggerHit;
    public AudioClip daggerTeleport;

    [Header("Extra")]
    public AudioClip openDoor;
    public AudioClip door;
    public AudioClip enemyDeath;
    public AudioClip elevator;
    public AudioClip lever;
    public AudioClip cat;
    public AudioClip waterSplash;
    public AudioClip skeleton;
    public AudioClip skletenAttack;
    public AudioClip skeletonDeath;
    public AudioClip skeletonHit;
    public AudioClip fallingBlock;

    [Header("UI")]
    public AudioClip lockedLvl;
    public AudioClip buttonPress;


    public bool isSFXPlaying = false;
    public bool isMusicPlaying = false;

    [SerializeField]public AudioSource SFXSource;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StopSFX()
    {
        //SFXSource.Stop();
        //Destroy(gameObject);
        isSFXPlaying = false;
    }

    public void PlaySFX(AudioClip clip)
    {
      //  if (clip != null && SFXSource != null)
        {
            isSFXPlaying = true;
            SFXSource.clip = clip;
            SFXSource.Play();
            Debug.Log("Playing SFX: " + clip.name);
            
        }


    }
}
