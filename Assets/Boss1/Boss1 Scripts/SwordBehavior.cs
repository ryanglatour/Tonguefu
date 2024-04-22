using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    public int maxcharge = 5;
    public int currentCharge = 0;
    public int attackDamage = 10; // Adjust the attack damage as needed
    private Animator animator; // Reference to the animator component
    public GameObject Sphere; // Assign the sphere GameObject in the Inspector
    public BeeHealth beeHealth;
    public ShieldHealth shieldHealth;
    void OnTriggerEnter(Collider other)
    {

       if (other.gameObject.CompareTag("BeeW")) // Check if the collider belongs to the bee
        {
            if(!IsSphereActive()){
                if (beeHealth != null)
                {
                    beeHealth.TakeDamage(attackDamage);
                }
            }
        }
        
        // Decrease shield's lives
        if (other.gameObject.CompareTag("Shield"))
        {
            if(IsSphereActive()){
                if (shieldHealth != null)
                {
                    shieldHealth.TakeDamage(attackDamage);
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
