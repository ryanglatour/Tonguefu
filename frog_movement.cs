using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float jumpForce = 8f; // Adjust the jump force as needed
    public Transform groundCheck; // Assign the transform of an empty GameObject positioned slightly below the frog to check if it's grounded
    public LayerMask groundLayer; // Assign the layer(s) that represent the ground

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Check if the frog is grounded
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, groundLayer);

        // Movement
        float moveInput = Input.GetAxis("Horizontal"); // Assuming you're using A and D or Left and Right arrow keys for movement
        Vector3 moveDirection = new Vector3(moveInput, 0f, 0f);
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, 0f);

        // Flip the frog's rotation based on movement direction
       // Flip if moving left
        if (moveInput < 0) {

            // Apply the rotation to the player model's transform instantly
            transform.rotation = Quaternion.Euler(-90f, 0f, -90f);
        }
        else if (moveInput > 0) {
            transform.rotation = Quaternion.Euler(-90f, 0f, 90f);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
