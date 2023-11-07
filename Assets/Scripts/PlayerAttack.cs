
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //[SerializeField] private PlayerControl playerControl;
    [SerializeField] private GameObject dagger;
    [SerializeField] public Transform shootingPoint;
    public bool activeDagger;
    public Animator playerAnim;
    [SerializeField] private Animator daggerAnim;
    [SerializeField] private bool isTeleporting;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        
    }

    private void Update()
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


            }

            
        }
    }

    private void StartAttack() //animation event
    {
        Attack();
    }

    private void Attack()
    {
        dagger.transform.position = shootingPoint.position;
        dagger.GetComponent<Dagger>().SetDirection(Mathf.Sign(transform.localScale.x));
        activeDagger = true; 
        StartCoroutine(Explode(0.35f));
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
        }
    }

    private void EndTeleportation()
    { 
        playerAnim.ResetTrigger("teleport");
        isTeleporting = false;
    }
}




