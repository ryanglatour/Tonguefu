using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    private Animator animator; // Reference to the Animator component attached to the sword

    void Start()
    {
        // Get the Animator component attached to the sword GameObject
        animator = GetComponent<Animator>();
    }

    // Method to trigger the attack animation
    public void TriggerAttackAnimation()
    {
        // Trigger the attack animation on the sword
        animator.SetTrigger("Attack");
    }
}

