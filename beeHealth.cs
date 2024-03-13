using UnityEngine;

public class BeeHealth : MonoBehaviour
{
    public int maxHealth = 5; // Adjust the maximum health as needed
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health when the bee is spawned
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Decrease current health by the damage amount

        if (currentHealth <= 0)
        {
            Die(); // If the bee's health drops to or below zero, call the Die method
        }
    }

    void Die()
    {
        // Add death behavior here (e.g., play death animation, destroy the bee GameObject, etc.)
        Destroy(gameObject); // Destroy the bee GameObject when it dies
    }
}

