using UnityEngine;

public class Stinger : MonoBehaviour
{
    public Transform frog; // Reference to the frog's transform
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 startAltPoint;
    public Vector3 endAltPoint;
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 180.0f; // Adjust rotation speed as needed
    private float startTime;
    private bool switched = false; // Flag to track if points have been switched
    private float switchThreshold = 8.21f; // Threshold for switching points

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // Check if the frog has passed the switch threshold
        if (!switched && frog.position.x >= switchThreshold)
        {
            // Switch the start and end points
            SwitchPoints();
        }
        else if (switched && frog.position.x < switchThreshold)
        {
            // Switch back to the original start and end points
            SwitchPoints();
        }

        // Calculate the current position based on the time elapsed since the start
        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / Vector3.Distance(startPoint, endPoint);
        transform.position = Vector3.Lerp(startPoint, endPoint, fractionOfJourney);

        // If the stinger reaches the end point, reset its position to the start point
        if (fractionOfJourney >= 1.0f)
        {
            transform.position = startPoint;
            startTime = Time.time; // Reset the start time to continue movement
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Frog"))
        {
            // If collision with frog, reset stinger's position to start point
            transform.position = startPoint;
            startTime = Time.time; // Reset the start time to continue movement
        }
    }

    // Method to switch between start and end points
    void SwitchPoints()
    {
        Vector3 tempStart = startPoint;
        startPoint = startAltPoint;
        startAltPoint = tempStart;

        Vector3 tempEnd = endPoint;
        endPoint = endAltPoint;
        endAltPoint = tempEnd;

        startTime = Time.time; // Reset the start time
        switched = !switched; // Toggle the switched flag
    }

}
