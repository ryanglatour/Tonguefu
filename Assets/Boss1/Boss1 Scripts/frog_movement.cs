using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float jumpForce = 8f; // Adjust the jump force as needed
    public float superJumpForce = 12f;
    public Transform groundCheck; // Assign the transform of an empty GameObject positioned slightly below the frog to check if it's grounded
    public LayerMask groundLayer; // Assign the layer(s) that represent the ground
    public LayerMask lilypadLayer; // Assign the layer(s) that represent the lilypad

    private Rigidbody rb;
    private bool isGrounded;
    private bool isOnLilypad;
    private Animator animator; // Reference to the animator component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        // Get the Animator component attached to the frog GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the frog is grounded
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, groundLayer);
        isOnLilypad = Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, lilypadLayer);

        // Movement
        float moveInput = Input.GetAxis("Horizontal"); // Assuming you're using A and D or Left and Right arrow keys for movement
        Vector3 moveDirection = new Vector3(moveInput, 0f, 0f);
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, 0f);

        // Flip the frog's rotation based on movement direction
        if (moveInput < 0) {
            // Apply the rotation to the player model's transform instantly
            transform.rotation = Quaternion.Euler(-90f, 0f, -90f);
        }
        else if (moveInput > 0) {
            transform.rotation = Quaternion.Euler(-90f, 0f, 90f);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Jumping
        if (isOnLilypad)
        {
            rb.AddForce(Vector3.up * superJumpForce, ForceMode.Impulse);
        }

        // Perform tongue animation when right mouse button is clicked
        if (Input.GetMouseButtonDown(1)) // 1 corresponds to the right mouse button
        {
            // Trigger the "tongue" animation
            animator.SetTrigger("Tongue");
        }

        // Perform tongue animation when right mouse button is clicked
        if (Input.GetMouseButtonDown(0)) // 1 corresponds to the right mouse button
        {
            // Trigger the "tongue" animation
            animator.SetTrigger("Attack");
        }
    }
}