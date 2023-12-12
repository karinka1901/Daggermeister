using UnityEngine;

public class Elevator: MonoBehaviour
{
    public Vector3 endPos;
    public float speed = 0.5f;
    public Vector3 startPos;
    private bool movingToEnd = true;
    public bool elevatorOn;
    private PlayerControl player;
    //private bool playerOnPlatform;
    private Transform playerPlaceholder;

    void Start()
    {
        transform.position = startPos;
        player = FindObjectOfType<PlayerControl>();

        playerPlaceholder = new GameObject("TempPLayerPos").transform;
        playerPlaceholder.parent = transform;
        playerPlaceholder.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (elevatorOn)
        {
            MoveElevator();
        }
        
    }

    void MoveElevator()
    {
        Vector3 direction = movingToEnd ? (endPos - transform.position) : (startPos - transform.position);

        float distance = direction.magnitude;

        direction.Normalize();

        transform.Translate(direction * speed * Time.deltaTime);

        if (distance < 0.01f)
        {
            movingToEnd = !movingToEnd;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //playerOnPlatform = true;
            playerPlaceholder.position = collision.transform.position;
            playerPlaceholder.gameObject.SetActive(true);
            collision.transform.parent = playerPlaceholder;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //playerOnPlatform = false;
            collision.transform.parent = null;
            playerPlaceholder.gameObject.SetActive(false);
        }
    }
}


    

