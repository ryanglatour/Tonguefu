using System.Collections;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GameObject hookPrefab; // Assign your grappling hook prefab in the inspector
    public Transform shootPoint; // Assign the point from which the hook shoots
    public float speed = 10f; // Speed at which the player is pulled towards the target

    private GameObject currentHook;
    private Transform targetObject;
    private LineRenderer lr;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Change 0 to 1 for right click, if preferred
        {
            if (currentHook != null) Destroy(currentHook);
            ShootHook();
        }
        if (currentHook != null)
        {
            lr.SetPosition(0, shootPoint.position);
            lr.SetPosition(1, targetObject.position);
        }
    }

    void ShootHook()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("GrappleTarget")) // Ensure your target has this tag
            {
                targetObject = hit.collider.transform;

                if (currentHook != null)
                {
                    Destroy(currentHook); // Destroy existing hook if there is one
                }

                // Instantiate the hook and set it up
                currentHook = Instantiate(hookPrefab, shootPoint.position, Quaternion.identity);
                lr = currentHook.GetComponent<LineRenderer>();
                lr.SetPosition(0, shootPoint.position);
                lr.SetPosition(1, targetObject.position);

                // Start moving towards the target
                //StartCoroutine(MoveTowardsTarget(hit.point));
            }
        }
    }

    IEnumerator MoveTowardsTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (currentHook != null)
            {
                // Update the hook's position as the player moves
                LineRenderer lr = currentHook.GetComponent<LineRenderer>();
                lr.SetPosition(0, shootPoint.position);
            }
            yield return null;
        }

        if (currentHook != null)
        {
            Destroy(currentHook); // Destroy the hook when the player reaches the target
        }
    }
}