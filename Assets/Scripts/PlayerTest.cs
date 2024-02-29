using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerTest : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb; 

    // Player model
    public Transform playerModel;
    float lastMovement;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Jump
    public bool isGrounded;
    public float jumpForce = 1f;
    
    // Tree
    public Transform target;

    // Enemy
    public float knockback = 20f;
    private bool canBeHit = true;
    private float hitCooldownTime = 0.75f;

    public GameObject winTextObject;


    // Start is called before the first frame update.
    void Start()
    {
        winTextObject.SetActive(false);
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

    }
 
    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }


    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate() 
    {
        // JUMP

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        
        if (rb.velocity.y < 0f && rb.velocity.y > -10f) 
			rb.velocity += new Vector3(0f, -Physics.gravity.y * (-1.5f) * Time.deltaTime, 0f);

        
        if (isGrounded && movementY > 0f)
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = new Vector3(0f, jumpForce, 0f);
            //rb.AddForce(Vector3.up * jumpForce);



        // PLAYER MODEL

        // Player model rotate
        
        // Flip if moving right
        if (lastMovement > 0) {

            // Apply the rotation to the player model's transform instantly
            playerModel.rotation = Quaternion.Euler(-90f, 0f, -90f);
        }
        else {
            playerModel.rotation = Quaternion.Euler(-90f, 0f, 90f);
        }

        if (rb.position.y > 20) winTextObject.SetActive(true);

        // Record last Movement
        if (movementX != 0)
            lastMovement = movementX;

    }

    // Hit cooldown
     IEnumerator HitCooldown()
    {
        // Disable knockback for the specified duration
        canBeHit = false;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(hitCooldownTime);

        // Enable knockback again
        canBeHit = true;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy") && canBeHit) {
            StartCoroutine(HitCooldown());
            Debug.Log("hit");

            Vector3 direction = (transform.position - other.transform.position).normalized;

            Debug.Log(direction);

            // Handle x knockback
            if (direction.x < 0) 
                target.transform.Rotate(0.0f, -direction.x * knockback, 0.0f);
            else
                target.transform.Rotate(0.0f, -direction.x * knockback, 0.0f);
            
            // Handle y knockback
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * knockback * direction.y * 0.35f);

            
                
                
        }
    }



 
}