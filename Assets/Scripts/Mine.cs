using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
//    Animator animator;


//    private void Start()
//    {
//        animator = GetComponent<Animator>();
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.tag == "Player")
//        {
//            animator.SetTrigger("Explode");
//        }
//    }

//    private void DeactivateMine()
//    {
//        gameObject.SetActive(false);
//    }
//}


//    public float speed = 2.0f; // Speed of the movement
//    public float amplitude = 1.0f; // How far the object moves up and down
//    private Vector3 startPos; // Initial position

//    void Start()
//    {
//        startPos = transform.position; // Save the initial position
//    }

//    void Update()
//    {
//        // Calculate the new position using a sine wave
//        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
//        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
//    }
//}



    public float speed = 2.0f; // Speed of the movement
    public float amplitude = 0.5f; // How far the object moves up and down

    void Update()
    {
        // Calculate the vertical movement using a sine wave
        float verticalMovement = Mathf.Sin(Time.time * speed) * amplitude;

        // Move the object up and down along the Y-axis
        transform.Translate(Vector3.up * verticalMovement * Time.deltaTime);
    }
}
