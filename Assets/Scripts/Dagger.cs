using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Dagger : MonoBehaviour
{
    [SerializeField] private float daggerSpeed;
    private bool hit;
    private float direction;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private Animator enemyAnim;
    public bool deactivated;
    [SerializeField]private PlayerAttack playerAttack;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = daggerSpeed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        if (!playerAttack.activeDagger)
        {
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            gameObject.SetActive(false);
            playerAttack.activeDagger = false;
            anim.SetTrigger("Explode");
        }
        if (collision.tag == "Collectable" || collision.tag == "EnemyWall" || collision.tag == "Key" || collision.tag == "Spikes")
        {
            hit = false;
        }
        else
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("Explode");
        }
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    private void Deactivate() //event for the animation
    {
        gameObject.SetActive(false);
        playerAttack.activeDagger = false;

    }



}
