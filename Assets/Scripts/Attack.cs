using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public MeshRenderer activeSword;
    public MeshRenderer passiveSword;

    private bool swinging = false;
    private bool canSwing = true;
     void Update() {

         if (Mouse.current.leftButton.wasPressedThisFrame && canSwing) {
            
             StartCoroutine(SwingTime());
             StartCoroutine(SwingCooldown());
             StartCoroutine(AnimateSwing());
             Debug.Log("click" + swinging);
         }
     }

     IEnumerator AnimateSwing()
     {
        activeSword.enabled = true;
        passiveSword.enabled = false;

        yield return new WaitForSeconds(0.2f);

        activeSword.enabled = false;
        passiveSword.enabled = true;

     }

     IEnumerator SwingCooldown()
     {
         // disable swinging
         canSwing = false;

         yield return new WaitForSeconds(0.3f);

         canSwing = true;
     }

     // Hit cooldown
     IEnumerator SwingTime()
    {
        // Disable knockback for the specified duration
        swinging = true;
        

        // Wait for the cooldown duration
        yield return new WaitForSeconds(0.05f);

        //
        swinging = false;
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
