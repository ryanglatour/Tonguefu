using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public MeshRenderer activeSword;
    public MeshRenderer passiveSword;

    private bool swinging = false;
     void Update() {

         if (Mouse.current.leftButton.wasPressedThisFrame) {
            
             StartCoroutine(SwingTime());
             Debug.Log("click" + swinging);
         }
     }

     // Hit cooldown
     IEnumerator SwingTime()
    {
        // Disable knockback for the specified duration
        swinging = true;
        activeSword.enabled = true;
        passiveSword.enabled = false;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(0.15f);

        //
        swinging = false;
        activeSword.enabled = false;
        passiveSword.enabled = true;
    }

     private void OnTriggerEnter(Collider other) {
         if (swinging && other.CompareTag("Enemy")) {
             Destroy(other.gameObject);
         }
     }
     

     private void OnTriggerStay(Collider other) {
         if (swinging && other.CompareTag("Enemy")) {
             Destroy(other.gameObject);
         }
     }
}
