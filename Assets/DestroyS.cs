using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject Sword;
     void Start()
    {
       Sword.SetActive(false);
    }
    // This function is called when the Collider other enters the trigger.
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is happening with the frog object.
        if (collision.gameObject.CompareTag("Frog"))
        {
            // If the collision is with the frog, destroy the sword object.
            Destroy(gameObject);
        }

        Sword.SetActive(true);
    }
}

