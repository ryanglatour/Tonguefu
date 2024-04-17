using UnityEngine;
using System.Collections;


public class ShieldHealth : MonoBehaviour
{
    public GameObject Heart;
    public GameObject Power;
    public GameObject Shield;
    public GameObject Slime;
    public GameObject Stinger;
    public int maxHealth = 9; // Adjust the maximum health as needed
    public int currentHealth { get; private set; } // Make currentHealth accessible with a public getter and private setter
    private Animator animator; // Reference to the Animator component
    public GameObject Sphere;
    public GameObject particleSystemObject_1; // Assign the particle system GameObject in the Inspector
    public GameObject particleSystemObject_2; // Assign the particle system GameObject in the Inspector
    private BeeHealth beeHealth;
    public GameObject Army_1;
    public GameObject Army_2;
    public GameObject Army_3;
    private bool objectsReappearing = false;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get reference to Animator component

        currentHealth = maxHealth; // Set current health to max health when the bee is spawned
        
        Heart.SetActive(false);
        Power.SetActive(false);
        
        // Ensure the particle systems are inactive at the beginning
        if (particleSystemObject_1 != null)
        {
            particleSystemObject_1.SetActive(false);
        }

        if (particleSystemObject_2 != null)
        {
            particleSystemObject_2.SetActive(false);
        }

    }


    public void TakeDamage(int damageAmount)
    {
        Debug.Log("It is taking damage\n");
        currentHealth -= damageAmount;
        Debug.Log("Lives Shield: " + currentHealth);

        if (currentHealth == 6 || currentHealth == 3 || currentHealth == 0)
        {
            if (!objectsReappearing)
            {
                StartCoroutine(ReappearObjectsAfterDelay(5f));
            }

            DisappearSphere();
            Stinger.SetActive(false);
            Slime.SetActive(false);
            animator.SetTrigger("Recover");

            particleSystemObject_1.SetActive(true);

            Army_1.SetActive(false);
            Army_2.SetActive(false);
            Army_3.SetActive(false);
        }
    }

    void DisappearSphere()
    {
        if (Sphere != null)
        {
            Sphere.SetActive(false);
        }
    }

    IEnumerator ReappearObjectsAfterDelay(float delay)
    {
        objectsReappearing = true;
        yield return new WaitForSeconds(delay);

        animator.SetTrigger("BackUp");

        // Reappear objects after delay
        Heart.SetActive(true);
        Power.SetActive(true);
        Sphere.SetActive(true);
        Stinger.SetActive(true);
        Slime.SetActive(true);

        Army_1.SetActive(true);
        Army_2.SetActive(true);
        Army_3.SetActive(true);

        objectsReappearing = false;
    }
}