using UnityEngine;

public class FrogHeadController : MonoBehaviour
{
    // Define minimum and maximum rotation angles for the head
    public float minRotation = -45f;
    public float maxRotation = 45f;

    // Define speed for the head rotation
    public float rotationSpeed = 5.0f;

    // Define the start rotation for the head
    private Quaternion startRotation;

    void Start()
    {
        // Store the initial rotation of the head
        startRotation = transform.localRotation;
    }

    void Update()
    {
        // Check for right mouse button down
        if (Input.GetMouseButtonDown(1))
        {
            // Get the position of the mouse click
            Vector3 mousePosition = -Input.mousePosition;
            mousePosition.z = transform.position.z - Camera.main.transform.position.z;

            // Convert the mouse position to world coordinates
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Calculate the direction from the frog's head to the mouse position
            Vector3 targetDirection = targetPosition - transform.position;
            targetDirection.z = 0; // Keep the rotation in 2D

            // Calculate the angle between the current forward direction and the target direction
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            // Clamp the angle to the specified range
            angle = Mathf.Clamp(angle, minRotation, maxRotation);

            // Apply the rotation
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        // Check for right mouse button up
        if (Input.GetMouseButtonUp(1))
        {
            // Reset the head to its start rotation
            transform.localRotation = startRotation;
        }
    }
}
