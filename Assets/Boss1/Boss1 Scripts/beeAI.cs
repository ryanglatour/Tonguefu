using UnityEngine;
using System.Collections;

public class BeeMovement : MonoBehaviour
{
    private Vector3 lastPosition;
    private Animator animator; // Reference to the Animator component
    public GameObject Beehive; // Assign the object to disappear in the Inspector

    void Start()
    {
        lastPosition = transform.position;
        animator = GetComponent<Animator>(); // Get reference to Animator component
        PlayLandingAnimation(); // Call the method to play the landing animation
 
    }


    // Method to play the landing animation
    private void PlayLandingAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Landing"); // Trigger the "Landing" animation
        }
    }


    void FixedUpdate()
    {
        // Ensure that the bee stays in its current Z position
        Vector3 currentPosition = transform.position;
        currentPosition.z = 0f; // Set Z position to 0
        transform.position = currentPosition;
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
