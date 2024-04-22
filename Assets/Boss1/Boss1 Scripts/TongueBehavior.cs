using UnityEngine;
using System.Collections;

public class TongueBehavior : MonoBehaviour
{
    public bool isSwallow = false;
    public int maxcharge = 5;
    public int attackDamage = 0; // Adjust the attack damage as needed
    private Animator animator; // Reference to the animator component
    public GameObject Sphere; // Assign the sphere GameObject in the Inspector
    public BeeHealth beeHealth;
    public ShieldHealth shieldHealth;
    public ChargeHealthBar chargeHealthBar;


    void Start (){
        chargeHealthBar.SetMaxHealth(maxcharge);
        chargeHealthBar.SetHealth(attackDamage);
        
    }
    void Update (){
        Debug.Log("IsSwallow = " + isSwallow);
        chargeHealthBar.SetHealth(attackDamage);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("BeeW")) // Check if the collider belongs to the bee
        {
            if(!IsSphereActive()){
                Debug.Log("Bee hit");
                if (beeHealth != null)
                {
                    beeHealth.TakeDamage(attackDamage);
                    attackDamage = 0;
                    Debug.Log("Current Charge back to 0");
                }
            }
        }
        
        // Decrease shield's lives
        if (other.gameObject.CompareTag("Shield"))
        {
            if(IsSphereActive()){
                Debug.Log("Shield hit");
                if (shieldHealth != null)
                {
                    shieldHealth.TakeDamage(attackDamage);
                    attackDamage = 0;
                }
            }
        }
    }

    public IEnumerator ResetIsSwallow()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        isSwallow = false; // Set isSwallow back to false
    }

    // Check if the sphere containing the bee is active
    bool IsSphereActive()
    {
        return Sphere != null && Sphere.activeSelf;
    }
}


