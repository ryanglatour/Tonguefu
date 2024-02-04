using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb; 

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0; 
    public float jumpForce = 1f;
    public bool isGrounded;

    private Vector3 offset;

    public Transform target;
    public Vector3 direction;

    public float fixedDistance = 8f;

    public float x = 0f;


    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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
        // Jump
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        
        if (isGrounded && movementY > 0)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Rotate around the target (cylinder) based on user input or time
        //transform.RotateAround(target.position, target.up, speed * (-movementX) * Time.deltaTime);

        x = movementX + x;
        WrapAroundObject(x / 100f);

        // Keep the player upright
        //transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        //Debug.Log("Rotation Axis: " + transform.rotation.eulerAngles);
    }

    void WrapAroundObject(float x)
    {
        // Implement wrapping logic based on the shape of your 3D object
        // For example, wrap around a sphere
        // (you may need to customize this based on your specific 3D object)
        float radius = 8f;

        float xPos = radius * Mathf.Cos(x);
        float yPos = transform.position.y;
        float zPos = radius * Mathf.Sin(x);

        Vector3 newPos = new Vector3(xPos, yPos, zPos);

        //if (!CheckForCollisions(newPos))
            transform.position = newPos;
    }

    bool CheckForCollisions(Vector3 positionToCheck)
    {
        // Set the radius of the sphere for overlap check
        float sphereRadius = 0.01f;

        // Get all colliders within the specified sphere
        Collider[] colliders = Physics.OverlapSphere(positionToCheck, sphereRadius);

        // Check if any colliders were found
        return colliders.Length > 0;
    }


}