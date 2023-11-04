
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMovement;
    [SerializeField] private GameObject dagger;
    [SerializeField] public Transform shootingPoint;
    public bool activeDagger;
    private Animator playerAnim;
    [SerializeField] private Animator daggerAnim;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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
        StartCoroutine(Explode(0.3f));
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
            activeDagger = false; 
        }
    }
}




