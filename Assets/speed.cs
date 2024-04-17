using UnityEngine;


public class SecondBeeMovement : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 180.0f; // Adjust rotation speed as needed
    private float startTime;
    private Animator animator; // Reference to the animator component

    void Start()
    {
        startTime = Time.time;
        // Get the Animator component attached to the frog GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Calculate the current position based on the time elapsed since the start
        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / Vector3.Distance(startPoint, endPoint);
        transform.position = Vector3.Lerp(startPoint, endPoint, fractionOfJourney);

        // If the bee has reached the end point, swap the start and end points, reset the start time, and rotate the bee
        if (fractionOfJourney >= 1.0f)
        {
            // Swap start and end points
            Vector3 temp = startPoint;
            startPoint = endPoint;
            endPoint = temp;

            // Reset start time
            startTime = Time.time;

            // Rotate the bee
            RotateBee();
        }
    }

    void RotateBee()
    {
        // Calculate the direction of movement
        Vector3 moveDirection = (endPoint - startPoint).normalized;

        // Rotate the bee based on movement direction
        if (moveDirection.x < 0) // Moving left
        {
            transform.rotation = Quaternion.Euler(-90f, 0f, 270f); // Rotate to face left
        }
        else if (moveDirection.x > 0) // Moving right
        {
            transform.rotation = Quaternion.Euler(-90f, 0f, 90f); // Rotate to face right
        }
    }
}
