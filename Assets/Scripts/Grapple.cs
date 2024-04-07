using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Grapple : MonoBehaviour
{
    public GameObject hookPrefab; // Assign your grappling hook prefab in the inspector
    public Transform shootPoint; // Assign the point from which the hook shoots
    public float speed = 10f; // Speed at which the player is pulled towards the target
    public Transform Tree;

    private GameObject currentHook;
    private Transform targetObject;
    private LineRenderer lr;
    private Rigidbody rb;
    private float movementX;

    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(rb.velocity.y);
        if (Input.GetMouseButtonDown(1)) // Change 0 to 1 for right click, if preferred
        {
            if (currentHook != null) Destroy(currentHook);
            ShootHook();
        }
        if (currentHook != null)
        {
            
            float distanceX = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(targetObject.position.x, 0f, targetObject.position.z));
            float distanceY = Vector3.Distance(new Vector3(0f, transform.position.y, 0f), new Vector3(0f, targetObject.position.y, 0f));
            Vector3 direction = (targetObject.position - transform.position);
            if (distanceX > 8f || distanceX < -8f) Destroy(currentHook);

            lr.SetPosition(0, shootPoint.position);
            lr.SetPosition(1, targetObject.position);

            // Bring player towards branch
            
            if (direction.y > 0 && rb.velocity.y < 7f)
            {
                rb.velocity += new Vector3(0f, 0.1f * distanceY, 0f);
            }
            if (direction.x < 0f) Tree.Rotate(0.0f, 0.015f * (distanceX * distanceX), 0.0f);
            if (direction.x > 0f) Tree.Rotate(0.0f, -0.015f * (distanceX * distanceX), 0.0f);

            
        }
    }

    void ShootHook()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 13f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            
            if (hit.collider.CompareTag("GrappleTarget")) // Ensure your target has this tag
            {
                Debug.Log("hit");
                targetObject = hit.collider.transform;
                //if (Vector3.Distance(transform.position, targetObject.position) > 8f) return;

                if (currentHook != null)
                {
                    Destroy(currentHook); // Destroy existing hook if there is one
                }

                // Instantiate the hook and set it up
                currentHook = Instantiate(hookPrefab, shootPoint.position, Quaternion.identity);
                lr = currentHook.GetComponent<LineRenderer>();
                lr.SetPosition(0, shootPoint.position);
                lr.SetPosition(1, targetObject.position);

                float distanceY = Vector3.Distance(new Vector3(0f, transform.position.y, 0f), new Vector3(0f, targetObject.position.y, 0f));
                rb.velocity += new Vector3(0f, 1.5f * distanceY, 0f);

                // Start moving towards the target
                //StartCoroutine(MoveTowardsTarget());
            }
        }
    }

    IEnumerator MoveTowardsTarget()
    {
        float distance = Vector3.Distance(transform.position, targetObject.position);

        while (distance > 2f)
        {
            Tree.transform.Rotate(0.0f, 5f * Time.deltaTime, 0.0f);



            yield return null;
        }

    }
}