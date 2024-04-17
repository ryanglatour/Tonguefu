using UnityEngine;

public class Poof : MonoBehaviour
{
    public GameObject Power_Up;

    // This function is called when the Collider other enters the trigger.
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is happening with the frog object.
        if (collision.gameObject.CompareTag("Frog"))
        {
            gameObject.SetActive(false);
        }
    }
}
