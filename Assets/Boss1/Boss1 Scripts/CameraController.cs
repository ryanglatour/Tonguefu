using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform frog; // Reference to the frog GameObject
    public float cameraOffset = 2f; // Offset from the frog position
    public float moveSpeed = 2f; // Speed at which the camera moves
    public float triggerPoint = 0f; // X position where the camera starts moving

    private Vector3 initialPosition; // Initial position of the camera
    private Vector3 targetPosition; // Target position for the camera

    private bool shouldMoveCamera = false; // Flag to indicate whether the camera should move

    void Start()
    {
        initialPosition = transform.position; // Store the initial position of the camera
    }

    void Update()
    {
        if (frog.position.x > triggerPoint)
        {
            shouldMoveCamera = true;
        }
        else if (frog.position.x < -triggerPoint) // Check if frog is past the trigger point in the opposite direction
        {
            shouldMoveCamera = false;
        }

        if (shouldMoveCamera)
        {
            // Calculate the target position for the camera
            targetPosition = new Vector3(initialPosition.x + cameraOffset, transform.position.y, transform.position.z);
        }
        else
        {
            // Move camera back to initial position
            targetPosition = initialPosition;
        }

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
