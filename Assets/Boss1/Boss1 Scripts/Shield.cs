using UnityEngine;
using System.Collections;


public class ShieldHealth : MonoBehaviour
{
    private bool isMoving = false; // Flag to track if the shield is currently moving
    public GameObject Heart;
    public GameObject Power;
    public GameObject Shield;
    public GameObject Slime;
    public GameObject Stinger;
    public int maxHealth = 3; // Adjust the maximum health as needed
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
    public coreHealthBar CoreHealthBar;
    private Animator animatorBee;
    private Animator animatorCore;
    public GameObject Bee;
    public GameObject Core;
    public GameObject Slime_2;
    public GameObject Avoid;
    public GameObject Heart_2;
    public GameObject Power_2;


    void Start()
    {
        Bee = GameObject.FindGameObjectWithTag("BeeW");
        animator = GetComponent<Animator>(); // Get reference to Animator component
        animatorBee = Bee.GetComponent<Animator>();
        animatorCore = Core.GetComponent<Animator>();

        currentHealth = maxHealth; // Set current health to max health when the bee is spawned

        
        Slime_2.SetActive(false);

        CoreHealthBar.SetMaxHealth(maxHealth);
        
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
        CoreHealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            if (!objectsReappearing)
            {
                StartCoroutine(ReappearObjectsAfterDelay(5f));
            }
            
            animatorBee.SetTrigger("Knock");
            DisappearSphere();
            Stinger.SetActive(false);
            
            

            particleSystemObject_1.SetActive(true);

            Army_1.SetActive(false);
            Army_2.SetActive(false);
            Army_3.SetActive(false);
            Avoid.SetActive(true);
            moveShield();
        }
    }

    public void moveShield(){
        if(!isMoving){
            Slime.SetActive(false);
            animator.SetTrigger("Recover");
            animatorCore.SetTrigger("Recover");
            Slime_2.SetActive(true);
            isMoving = true;
        }
        else if (isMoving){
            Slime_2.SetActive(false);
            animator.SetTrigger("Rigth");
            animatorCore.SetTrigger("Rigth");
            Slime.SetActive(true);
            isMoving = false;

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
        animatorBee.SetTrigger("Alive");

        particleSystemObject_1.SetActive(true);

        // Reappear objects after delay
        Heart.SetActive(true);
        Power.SetActive(true);
        Avoid.SetActive(false);
        Sphere.SetActive(true);
        Stinger.SetActive(true);
        Heart_2.SetActive(true);
        Power_2.SetActive(true);

        particleSystemObject_1.SetActive(false);

        Army_1.SetActive(true);
        Army_2.SetActive(true);
        Army_3.SetActive(true);
        

        objectsReappearing = false;

        currentHealth = 10;
        CoreHealthBar.SetHealth(currentHealth);
    }
}