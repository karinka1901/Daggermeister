using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerControl player;
    [SerializeField] private GameObject dagger;
    [SerializeField] public Transform shootingPoint;
    public bool activeDagger;
    public Animator playerAnim;
    [SerializeField] private Animator daggerAnim;
    [SerializeField] private bool isTeleporting;
    SFXcontrol audioManager;
    GameManager gameManager;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        player = GetComponent<PlayerControl>();
        audioManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXcontrol>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {

        if (!player.pauseOn)
        {
            {
                if (Input.GetButtonDown("Fire1"))

                {
                    if (isTeleporting)
                    {
                        EndTeleportation();
                    }

                    if (activeDagger)
                    {
                        TeleportToDagger();
                    }
                    else
                    {
                        playerAnim.SetTrigger("Throw");
                       audioManager.PlaySFX(audioManager.daggerThrow);//////////////////////////////////SFX//////////
                    }

                }
            }
        }
        else
        {
            Debug.Log("CantAttack");
        }
    }

    private void StartAttack() //animation event
    {
        if (!gameManager.pauseOn && !player.isDead)
        {
            Attack();
            audioManager.PlaySFX(audioManager.daggerTeleport);//////////////////////////////////SFX//////////
        }
        else
        {
            Debug.Log("cant Attack");
        }
    }

    private void Attack()
    {
        dagger.transform.position = shootingPoint.position;
        dagger.GetComponent<Dagger>().SetDirection(Mathf.Sign(transform.localScale.x));
        activeDagger = true; 
        StartCoroutine(Explode(0.46f));
    }

    private IEnumerator Explode(float delay)
    {
        yield return new WaitForSeconds(delay);
        daggerAnim.SetTrigger("Poof");

    }

    private void TeleportToDagger()
    {
        if (activeDagger)
        {
            transform.position = dagger.transform.position;
            playerAnim.SetTrigger("teleport");
            activeDagger = false;
            isTeleporting = true;
           audioManager.PlaySFX(audioManager.daggerTeleport);//////////////////////////////////SFX//////////
        }
    }

    private void EndTeleportation()
    { 
        playerAnim.ResetTrigger("teleport");
        isTeleporting = false;
    }
}




