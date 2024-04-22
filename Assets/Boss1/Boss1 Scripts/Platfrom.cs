using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float moveSpeed = 2.0f;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
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
        }
    }

}