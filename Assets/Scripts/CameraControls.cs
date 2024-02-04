using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraControls : MonoBehaviour
{   
    public Transform target;
    public Transform player;

    public float smoothing = 10.0f;
    public float radius = 20f;


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 treeLocation = target.position;
        treeLocation.y = player.position.y;



        transform.LookAt(new Vector3(0, player.position.y, 0));

        Vector3 newPosition = WrapAroundObject(player.position);
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.deltaTime);

    }

    Vector3 WrapAroundObject(Vector3 position)
    {
        // Implement wrapping logic based on the shape of your 3D object
        // For example, wrap around a sphere
        // (you may need to customize this based on your specific 3D object)
       
        position.y = 0;
        position = position.normalized * radius;

        position.y = player.position.y;

        return position;
    }


}
