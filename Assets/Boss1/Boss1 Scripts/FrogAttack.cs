using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttack : MonoBehaviour
{
    public GameObject Power;
    public GameObject Sword; // Assign the sword GameObject in the Inspector
    public GameObject Tongue;
    public float attackRange_sword = 1.5f; // Adjust the attack range as needed

    public float attackRange_tongue = 1f;
    public int attackDamage = 10; // Adjust the attack damage as needed

    void Start()
    {
    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) // 0 represents left mouse button
        {
            // Call the Attack method
            Attack();
        }
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(1)) // 0 represents left mouse button
        {
            // Call the Attack method
            Attack_2();
        }      
    }

    void Attack()
    {
        // Perform attack check
        Collider[] colliders = Physics.OverlapSphere(Sword.transform.position, attackRange_sword);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Bee")) // Check if the collider belongs to the bee
            {
                // Apply damage to the bee
                BeeHealth beeHealth = collider.gameObject.GetComponent<BeeHealth>();
                if (beeHealth != null)
                {
                    beeHealth.TakeDamage(attackDamage);
                    // If the bee is hit by the attack, call the HitByAttack method of BeeMovement script
                    collider.gameObject.GetComponent<BeeMovement>().HitByAttack();
                }
            }

            if(collider.gameObject.CompareTag("Power")){
                Power.SetActive(false);
            }
        }
    }

    void Attack_2()
    {
        // Perform attack check
        Collider[] colliders = Physics.OverlapSphere(Tongue.transform.position, attackRange_tongue);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Bee")) // Check if the collider belongs to the bee
            {

                // Apply damage to the bee
                BeeHealth beeHealth = collider.gameObject.GetComponent<BeeHealth>();
                if (beeHealth != null)
                {
                    beeHealth.TakeDamage(attackDamage);
                    // If the bee is hit by the attack, call the HitByAttack method of BeeMovement script
                    collider.gameObject.GetComponent<BeeMovement>().HitByAttack();
                }
            }

            if(collider.gameObject.CompareTag("Power")){
                Power.SetActive(false);
            }
        }
    }

    
}