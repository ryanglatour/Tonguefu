using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttack : MonoBehaviour
{
    public GameObject Sphere; // Assign the sphere GameObject in the Inspector
    public GameObject Sword; // Assign the sword GameObject in the Inspector
    public float attackRange_sword = 1.5f; // Adjust the attack range as needed
    public float attackRange_tongue = 1f;
    public int attackDamage = 1; // Adjust the attack damage as needed
    private Animator animator; // Reference to the animator component
    public bool isAnimate = false;
    void Start()
    {
        // Get the Animator component attached to the frog GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!PauseMenu.isPaused){
            // Check for left mouse button click
            if (Input.GetMouseButtonDown(0)) // 0 represents left mouse button
            {
                // Trigger the "tongue" animation
                animator.SetTrigger("Attack");
                // Call the Attack method
                Attack();
            }
            // Check for left mouse button click
            if (Input.GetMouseButtonDown(1)) // 1 represents right mouse button
            {
                isAnimate = true;
                // Trigger the "tongue" animation
                animator.SetTrigger("Tongue");
                isAnimate = false;
            }    
        }
    }

    void Attack()
{
    // Perform attack check
    Collider[] colliders = Physics.OverlapSphere(Sword.transform.position, attackRange_sword);
    foreach (Collider collider in colliders)
    {
        if (collider.gameObject.CompareTag("BeeW")) // Check if the collider belongs to the bee
        {
            if(!IsSphereActive()){
                Debug.Log("Bee hit");
                // Apply damage to the bee
                BeeHealth beeHealth = collider.gameObject.GetComponent<BeeHealth>();
                if (beeHealth != null)
                {
                    beeHealth.TakeDamage(attackDamage);
                }
            }
            
        }
        
        // Decrease shield's lives
        if (collider.gameObject.CompareTag("Shield"))
        {
            if(IsSphereActive()){
                Debug.Log("Shield hit");
                ShieldHealth shieldHealth = collider.gameObject.GetComponent<ShieldHealth>();
                if (shieldHealth != null)
                {
                    shieldHealth.TakeDamage(attackDamage);
                }
            }
        }
    }
}

// Check if the sphere containing the bee is active
    bool IsSphereActive()
    {
        return Sphere != null && Sphere.activeSelf;
    }
}
