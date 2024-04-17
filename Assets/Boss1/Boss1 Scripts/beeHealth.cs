using UnityEngine;

public class BeeHealth : MonoBehaviour
{
    
    public GameObject Bee;
    public int maxHealth = 9; // Adjust the maximum health as needed
    public int currentHealth { get; private set; } // Make currentHealth accessible with a public getter and private setter
    private Animator animator; // Reference to the Animator component
    public GameObject Sphere;
    public GameObject Stinger;
    public GameObject Slime;
    public GameObject Army_1;
    public GameObject Army_2;
    public GameObject Army_3;
    public GameObject particleSystemObject_1; // Assign the particle system GameObject in the Inspector
    public GameObject particleSystemObject_2; // Assign the particle system GameObject in the Inspector
    public BeeHealthBar beeHealthBar;
    

    void Start()
    {

        particleSystemObject_1.SetActive(false);
        particleSystemObject_2.SetActive(false);
        currentHealth = maxHealth; // Set current health to max health when the bee is spawned
        animator = GetComponent<Animator>(); // Get reference to Animator component
        beeHealthBar.SetMaxHealth(maxHealth);

    }



    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Decrease current health by the damage amount
        beeHealthBar.SetHealth(currentHealth);
        Debug.Log("Bee current health: " + currentHealth); // Debug log to show current health

        if (currentHealth == 9 || currentHealth == 6 || currentHealth == 3)
        {

          particleSystemObject_1.SetActive(false);
          animator.SetTrigger("Alive");
          
        }
    }
}
