using UnityEngine;
using UnityEngine.UI; // Add this line if you want to update a UI element for displaying lives

public class FrogMovement : MonoBehaviour
{
    public int startingLives = 20; // Adjust the starting lives as needed
    public int lives; // Variable to store the frog's lives
    public int maxLives = 10; // Adjust the maximum lives as needed
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float jumpForce = 8f; // Adjust the jump force as needed
    public float superJumpForce = 12f;
    public float fallMultiplier = 2.5f; // Adjust the fall multiplier for faster descent
    public float lowJumpMultiplier = 2f; // Adjust the low jump multiplier for a more natural jump arc
    public float pushbackForce = 5f; // Adjust the pushback force as needed
    public Transform groundCheck; // Assign the transform of an empty GameObject positioned slightly below the frog to check if it's grounded
    public LayerMask groundLayer; // Assign the layer(s) that represent the ground
    public GameObject frog;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isOnLilypad;
    
    private bool isInvincible = false; // Flag to track invincibility state
    public float invincibilityDuration = 10f; // Duration of invincibility in seconds
    private float invincibilityTimer; // Timer for invincibility duration
    public HealthBar healthBar;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        lives = startingLives; // Initialize lives to startingLives
        healthBar.SetMaxHealth(startingLives);
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
        if (moveInput < 0) {
            // Apply the rotation to the player model's transform instantly
            transform.rotation = Quaternion.Euler(-90f, 0f, -90f);
        }
        else if (moveInput > 0) {
            transform.rotation = Quaternion.Euler(-90f, 0f, 90f);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            }
            else if (isOnLilypad)
            {
                rb.velocity = new Vector3(rb.velocity.x, superJumpForce, 0f);
            }
        }

        // Apply gravity modifications for more natural jumping and falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Update invincibility timer
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }

    // Function to handle collision with bees
    private void OnCollisionEnter(Collision collision)
    {
        if (!isInvincible && collision.gameObject.CompareTag("Bee"))
        {
            // Reduce lives
            lives--;
            healthBar.SetHealth(lives);
            Debug.Log("Frog's Lives : " + lives);

            // Optionally, you can check if lives are zero and handle game over here
            if (lives <= 0)
            {
                // Game over logic goes here
                Debug.Log("Game Over!");
                //Destroy(frog);
            }

            // Destroy the bee object upon collision with the frog
            //Destroy(collision.gameObject);

            // Calculate pushback direction
            Vector3 pushbackDirection = transform.position - collision.transform.position;
            pushbackDirection.y = 0f; // Ignore vertical component

            // Apply pushback force
            rb.AddForce(pushbackDirection.normalized * pushbackForce, ForceMode.Impulse);

        }
        // Check if the collision is happening with the heart object.
        if (collision.gameObject.CompareTag("Heart"))
        {
            // Increase frog's health
            IncreaseHealth();

        }

        // Check if the collision is happening with the heart object.
        if (collision.gameObject.CompareTag("Power"))
        {
            // Apply invincibility
            ApplyInvincibility();
        }
    }

    // Method to increase frog's health
    private void IncreaseHealth()
    {
        if (lives < maxLives)
        {
            lives++;
            Debug.Log("Frog's health increased! Current health: " + lives);
        }
        else
        {
            Debug.Log("Frog's health is already at maximum!");
        }
    }

    // Method to apply invincibility effect
    private void ApplyInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }
}
