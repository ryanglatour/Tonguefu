using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FrogAttack : MonoBehaviour
{
    public GameObject Sword; // Assign the sword GameObject in the Inspector
    public float attackRange = 1.5f; // Adjust the attack range as needed
    public int attackDamage = 10; // Adjust the attack damage as needed
    private SwordAnimation swordAnimation; // Reference to the SwordAnimation script

   void Start()
    {
        // Get the SwordAnimation component attached to the sword GameObject
        swordAnimation = Sword.GetComponent<SwordAnimation>();
    }
    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) // 0 represents left mouse button
        {
            // Call the Attack method
            Attack();
        }   
    }

    void Attack()
    {
        // Trigger the attack animation on the sword
        swordAnimation.TriggerAttackAnimation();

        // Perform attack check
        Collider[] colliders = Physics.OverlapSphere(Sword.transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Bee")) // Check if the collider belongs to the bee
            {
                // Apply damage to the bee
                BeeHealth beeHealth = collider.gameObject.GetComponent<BeeHealth>();
                if (beeHealth != null)
                {
                    beeHealth.TakeDamage(attackDamage);
                }
            }
        }
    }
}

