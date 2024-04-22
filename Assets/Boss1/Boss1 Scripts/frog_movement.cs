using UnityEngine;
using UnityEngine.UI; // Add this line if you want to update a UI element for displaying lives
using UnityEngine.SceneManagement;

public class FrogMovement : MonoBehaviour
{
    public int startingLives = 20; // Adjust the starting lives as needed
    public int lives; // Variable to store the frog's lives
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
    public InveHealthBar inveHealthBar;
    public TongueBehavior tongueBehavior;
    private bool isSwallow;
    public GameObject Lose;


    void Start()
    {
        Lose.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        lives = startingLives; // Initialize lives to startingLives
        healthBar.SetMaxHealth(startingLives);
        inveHealthBar.SetMaxHealth(invincibilityDuration);
        inveHealthBar.SetVisibility(false); // Dont show the health bar slider
    }

    void Update()
    {
        // Check if the frog is grounded by casting a ray downwards
        // Check if the frog is grounded
        // Check if the frog is grounded by casting a ray downwards from multiple points
        isGrounded = GroundCheck();
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
            inveHealthBar.SetVisibility(true); // Show the health bar slider
            invincibilityTimer -= Time.deltaTime;
            inveHealthBar.SetHealth(invincibilityTimer);
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                inveHealthBar.SetMaxHealth(invincibilityDuration);
                inveHealthBar.SetVisibility(false); // Show the health bar slider
            }
        }

        if(lives <= 0){
            Time.timeScale = 0f;
            Lose.SetActive(true);
        }
    }

    bool GroundCheck()
{
    // Define an array of points for ground checking
    Vector3[] groundCheckPoints = new Vector3[9];
    groundCheckPoints[0] = groundCheck.position; // Center point
    groundCheckPoints[1] = new Vector3(groundCheck.position.x + 0.5f, groundCheck.position.y, groundCheck.position.z); // Right point
    groundCheckPoints[2] = new Vector3(groundCheck.position.x - 0.5f, groundCheck.position.y, groundCheck.position.z); // Left point
    groundCheckPoints[3] = new Vector3(groundCheck.position.x, groundCheck.position.y, groundCheck.position.z + 0.5f); // Forward point
    groundCheckPoints[4] = new Vector3(groundCheck.position.x, groundCheck.position.y, groundCheck.position.z - 0.5f); // Backward point
    groundCheckPoints[5] = new Vector3(groundCheck.position.x + 0.35f, groundCheck.position.y, groundCheck.position.z + 0.35f); // Forward-right point
    groundCheckPoints[6] = new Vector3(groundCheck.position.x - 0.35f, groundCheck.position.y, groundCheck.position.z + 0.35f); // Forward-left point
    groundCheckPoints[7] = new Vector3(groundCheck.position.x + 0.35f, groundCheck.position.y, groundCheck.position.z - 0.35f); // Backward-right point
    groundCheckPoints[8] = new Vector3(groundCheck.position.x - 0.35f, groundCheck.position.y, groundCheck.position.z - 0.35f); // Backward-left point

    foreach (Vector3 point in groundCheckPoints)
    {
        // Check if any of the points are in contact with the ground
        if (Physics.Raycast(point, Vector3.down, 0.1f, groundLayer))
        {
            return true;
        }
    }

    return false;
}


    // Function to handle collision with bees
    private void OnCollisionEnter(Collision collision)
    {
        isSwallow = tongueBehavior.isSwallow;
        
        if(isSwallow != null){
            bool isSwallow = tongueBehavior.isSwallow;

            if (!isInvincible && collision.gameObject.CompareTag("Bee") || !isInvincible && !isSwallow && collision.gameObject.CompareTag("Stinger") )
        {
            // Reduce lives
            lives--;
            healthBar.SetHealth(lives);
            Debug.Log("Frog's curent health = " + lives);

            // Calculate pushback direction from the frog to the bee
            Vector3 pushbackDirection = transform.position - collision.transform.position;
            pushbackDirection.Normalize(); // Normalize the vector to get a direction without changing its magnitude

            // Apply pushback force to the frog
            rb.AddForce(pushbackDirection * pushbackForce, ForceMode.Impulse);
        }
    
        

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


        // Check if the collision is happening with the heart object.
        if (collision.gameObject.CompareTag("Sphere"))
        {
            // Calculate pushback direction from the frog to the bee
            Vector3 pushbackDirection = transform.position - collision.transform.position;
            pushbackDirection.Normalize(); // Normalize the vector to get a direction without changing its magnitude

            // Apply pushback force to the frog
            rb.AddForce(pushbackDirection * pushbackForce, ForceMode.Impulse);
        }
    }

    

    // Method to increase frog's health
    private void IncreaseHealth()
    {
        if (lives <= startingLives)
        {
            Debug.Log("Frog's Healh was = " + lives);
            lives += 3;
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
