using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BetterJump : MonoBehaviour
{
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

	Rigidbody rb; 

	// This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

	void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		if (rb.velocity.y < 0) {
			rb.velocity += Vector3.up * -9.81f * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if (rb.velocity.y > 0 && movementY <= 0) {
			rb.velocity += Vector3.up * -9.81f * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}
}
