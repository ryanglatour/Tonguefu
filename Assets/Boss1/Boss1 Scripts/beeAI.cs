using UnityEngine;
using System.Collections;

public class BeeMovement : MonoBehaviour
{
    public GameObject lilyPad;
    public GameObject frog; // Assign the frog GameObject in the Inspector\
    public GameObject particleSystemObject_1; // Assign the particle system GameObject in the Inspector
    public GameObject particleSystemObject_2; // Assign the particle system GameObject in the Inspector
    public float movementSpeed = 5f; // Adjust the speed as needed
    public float pushbackForce = 2f; // Adjust the pushback force as needed
    public float hitRecoveryTime = 1.5f; // Adjust the hit recovery time as needed
    private Vector3 lastPosition;
    private bool isHit = false; // Flag to indicate if the bee is hit by the frog's attack
    private float hitTimer = 0f; // Timer to track the duration of hit recovery
    private Animator animator; // Reference to the Animator component
    private bool isFollowing = false; // Flag to indicate if the bee is following the frog
    public GameObject Beehive; // Assign the object to disappear in the Inspector
    private int hitCount = 0; // Variable to track the number of hits

    void Start()
    {
        lilyPad.SetActive(false);
        lastPosition = transform.position;
        animator = GetComponent<Animator>(); // Get reference to Animator component
        PlayLandingAnimation(); // Call the method to play the landing animation
        StartCoroutine(StartFollowingAfterDelay(9.4f)); // Start following after 5 seconds

        // Ensure the particle system is inactive at the beginning
        if (particleSystemObject_1 != null)
        {
            particleSystemObject_1.SetActive(false);
        }

         // Ensure the particle system is inactive at the beginning
        if (particleSystemObject_2 != null)
        {
            particleSystemObject_2.SetActive(false);
        }
 
    }

    // Coroutine to start following the frog after a delay
    IEnumerator StartFollowingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFollowing = true;
    }


    // Method to play the landing animation
    private void PlayLandingAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Landing"); // Trigger the "Landing" animation
        }
    }

    
    void Update()
    {
        // If the bee is not hit, the landing animation is not playing, and it's following time, start following the frog
        if (!isHit && isFollowing)
        {
            FollowFrog();
        }

        // Handle hit recovery logic
        if (isHit)
        {
            ApplyPushback();
        }
    }


    // Method to follow the frog
    private void FollowFrog()
    {
        if (frog != null)
        {
            if (isFollowing)
            {
                Vector3 currentPosition = transform.position;
                Vector3 frogPosition = frog.transform.position;
                // Ignore changes in the z-coordinate
                frogPosition.z = currentPosition.z;
                Vector3 moveDirection = (frogPosition - currentPosition).normalized;

                // Move the bee towards the frog's position along the x-axis
                transform.position += moveDirection * movementSpeed * Time.deltaTime;

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
            else
            {
                // Stop moving the bee
                transform.position = lastPosition;
            }
        }
    }

    // Method to apply pushback force when hit by the frog's attack
    private void ApplyPushback()
    {
        // Calculate pushback direction away from frog
        Vector3 pushbackDirection = (transform.position - frog.transform.position).normalized;

        // Apply pushback force to move away from frog
        transform.position += pushbackDirection * pushbackForce;

        // Update hit timer
        hitTimer += Time.deltaTime;

        // Check if hit recovery time has elapsed
        if (hitTimer >= hitRecoveryTime)
        {
            isHit = false; // Reset hit flag
            hitTimer = 0f; // Reset hit timer
        }
    }

    // Method to handle the frog's attack hitting the bee and applying pushback
    public void HitByAttack()
    {
       if (!isHit) // Only apply pushback if the bee is not already hit
    {
        isHit = true; // Set hit flag to true
        hitCount++; // Increase hit count
            if (hitCount >= 3) // If hit three times, stop following and play animation
            {
                StopFollowingAndAnimate();
                lilyPad.SetActive(true);
                
            }
       else
            {
                // Apply pushback force away from frog
                ApplyPushback();
            }
        }
    }

    // Method to make the bee stop following and play an animation
    private void StopFollowingAndAnimate()
    {
        // Stop following the frog
        isFollowing = false;

        // Activate particle system
        if (particleSystemObject_1 != null)
        {
            particleSystemObject_1.SetActive(true);
        }

        if (particleSystemObject_2 != null)
        {
            particleSystemObject_2.SetActive(true);
        }

        // Play animation
        if (animator != null)
        {
            animator.SetTrigger("StopFollowing"); // Trigger the "StopFollowing" animation
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the one to disappear
        if (collision.gameObject == Beehive)
        {
            // Destroy the object
            Destroy(Beehive);
        }
    }
}
