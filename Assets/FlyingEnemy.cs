using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D batRb;
    [HideInInspector] private BoxCollider2D batCollider;
    [HideInInspector] public Animator batAnim;
    private PlayerControl player;

    [HideInInspector] public float batSpeed = 0.4f;
    public float followRange = 2f;

    void Start()
    {
        batAnim = GetComponent<Animator>();
        batCollider = GetComponent<BoxCollider2D>();
        batRb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerControl>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 playerDirection = player.transform.position - transform.position;
            float distanceToPlayer = playerDirection.magnitude;

            if (distanceToPlayer <= followRange)
            {
                Vector3 direction = playerDirection.normalized;

               
                FlipBat(direction.x);

                transform.Translate(direction * batSpeed * Time.deltaTime);
                batAnim.SetBool("Follow", true);
            }
            else
            {
                batAnim.SetBool("Follow", false);
            }
        }
    }

    private void FlipBat(float directionX)
    {
        if (directionX < 0) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (directionX > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dagger")
        {
            batAnim.SetTrigger("Dead");
        }
    }

    public void DestroyBat()
    {
        Destroy(this.gameObject);
    }

    private void DisableBatCollider()
    {
        batCollider.enabled = false;
    }
}
