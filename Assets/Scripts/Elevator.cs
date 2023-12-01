using UnityEngine;

public class Elevator: MonoBehaviour
{
    public Vector3 endPos;
    public float speed = 0.5f;
    public Vector3 startPos;
    private bool movingToEnd = true;
    public bool elevatorOn;

    void Start()
    {
        transform.position = startPos; 
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

    
}
