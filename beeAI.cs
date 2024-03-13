using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    public GameObject frog; // Assign the frog GameObject in the Inspector
    public float movementSpeed = 5f; // Adjust the speed as needed

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (frog != null) // Make sure the frog reference is valid
        {
            // Get the current position of the bee and the frog
            Vector3 currentPosition = transform.position;
            Vector3 frogPosition = frog.transform.position;

            // Calculate the direction to move towards (only along the x-axis)
            Vector3 moveDirection = new Vector3(frogPosition.x - currentPosition.x, 0f, 0f).normalized;

            // Move the bee towards the frog's position along the x-axis
            transform.position += moveDirection * movementSpeed * Time.deltaTime;

            // Rotate the bee based on movement direction
            if (moveDirection.x < 0) // Moving left
            {
                transform.rotation = Quaternion.Euler(0f, -90f, 0f); // Rotate to face left
            }
            else if (moveDirection.x > 0) // Moving right
            {
                transform.rotation = Quaternion.Euler(0f, 90f, 0f); // Rotate to face right
            }
        }
    }
}



