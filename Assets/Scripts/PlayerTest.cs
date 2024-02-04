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

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0; 
 
    // Target
    public Transform target;

    public bool isGrounded;
    public float jumpForce = 1f;
    public float pushForce = 1f;
    public float range = 8.5f;
    private bool stay = true;

    private float time = 5f;

    public float maxSpeed = 10f;
    
    public GameObject winTextObject;

    float lastMovement;


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
        // Max speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        
        
        // Bring back to middle
        if (stay == false){
            //Debug.Log("Push Center Trigger");
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0;
            directionToTarget.Normalize();

            
            time += Time.deltaTime;
            rb.AddForce(directionToTarget * pushForce * time, ForceMode.Impulse);
        }

        // Jump
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        

        /*if (rb.velocity.y < -1f) 
			rb.velocity += new Vector3(0f, -Physics.gravity.y * (-1.25f) * Time.deltaTime, 0f);*/

        
        if (isGrounded && movementY > 0f)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.AddForce(Vector3.up * jumpForce);

        //target.position.y = transform.position.y;
        // Calculate the direction from the character to the target
        Vector3 toTarget = target.position - transform.position;

        // Calculate the tangent vector
        Vector3 tangent = new Vector3(-toTarget.z, 0, toTarget.x).normalized;



        // Player model rotate
        Quaternion targetRotation = Quaternion.LookRotation(tangent, Vector3.up);

        // Apply the rotation to the player model's transform instantly
        playerModel.rotation = Quaternion.Euler(-90f, targetRotation.eulerAngles.y, 0f);
        
        // Flip if moving right
        if (lastMovement > 0) {
            // Reverse the rotation by adding 180 degrees to the y-axis rotation
            float reversedRotation = targetRotation.eulerAngles.y + 180f;

            // Apply the rotation to the player model's transform instantly
            playerModel.rotation = Quaternion.Euler(-90f, reversedRotation, 0f);
        }



        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        
        rb.AddForce(-tangent * speed * movementX); 

        if (rb.position.y > 20) winTextObject.SetActive(true);

        // Record last Movement
        if (movementX != 0)
            lastMovement = movementX;
    }

    void OnTriggerStay(Collider other) {
        //Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();

        if (other.gameObject.CompareTag("PushAway")) {
            //Debug.Log("Push Away Trigger");
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = transform.position.y;
            directionToTarget.Normalize();

            

            rb.AddForce(-directionToTarget * pushForce, ForceMode.Impulse);
        }
        if (other.gameObject.CompareTag("PushCenter")) {
            time = 5f;
            stay = true;
        }

    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("PushCenter")) stay = false;
        
    }

 
}