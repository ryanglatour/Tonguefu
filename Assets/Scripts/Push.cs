using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public Transform target;
    public bool away = false;
    public float pushForce = 1f;

    private void onTriggerEnter(Collider other) {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        Debug.Log("Trigger");
        print("trigger");

        if (otherRigidbody != null)
        {
            Vector3 directionToTarget = target.position - other.transform.position;
            directionToTarget.Normalize();

            

            otherRigidbody.AddForce(directionToTarget * pushForce, ForceMode.Impulse);
        }
    }
}
