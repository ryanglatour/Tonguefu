using UnityEngine;

public class BeeHealth : MonoBehaviour
{
    public GameObject Bee;
    public int maxHealth = 9; // Adjust the maximum health as needed
    public int currentHealth { get; private set; } // Make currentHealth accessible with a public getter and private setter
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health when the bee is spawned
        animator = GetComponent<Animator>(); // Get reference to Animator component
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Decrease current health by the damage amount

        if (currentHealth <= 0)
        {
           Destroy(Bee); // If the bee's health drops to or below zero, call the Die method
        }
    }

   
}
