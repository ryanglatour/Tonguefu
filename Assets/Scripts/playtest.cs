using UnityEngine;

public class playtest : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = inputDirection * speed * Time.deltaTime;

        transform.Translate(movement);

        // Wrap the player around the 3D object (e.g., sphere)
        transform.position = WrapAroundObject(transform.position);
    }

    Vector3 WrapAroundObject(Vector3 position)
    {
        // Implement wrapping logic based on the shape of your 3D object
        // For example, wrap around a sphere
        // (you may need to customize this based on your specific 3D object)
        float radius = 7.5f;
        position = position.normalized * radius;

        return position;
    }
}
