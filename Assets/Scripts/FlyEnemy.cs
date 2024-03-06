using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public Transform player;
    public Transform spawn;

    public Rigidbody rb;
    public GameObject zone;
    
    private Vector3 zonePosition;

    public bool inZone = false;

    private Vector3 direction;

    private bool lastLeft = true;

    private Transform childObject;


    // Start is called before the first frame update
    void Start()
    {
        childObject = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

        // If player is not in zone
        if (!inZone) {
            direction = transform.position - spawn.transform.position;
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(spawn.transform.position.x, 0f, spawn.transform.position.z));
            
            float angle = Vector3.Angle(transform.forward, spawn.transform.position);

            Debug.Log(angle + " distance: " + distance);

            if (angle < 90f && distance > 1.5f) flyLeft();
            else if (angle > 90f && distance > 1.5f) flyRight();
            /*
            // Fly towards player horizontally
            if (direction.x / direction.z < -.01f) {
                flyRight();
                //transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
            else {
                flyLeft();
                //transform.rotation = Quaternion.Euler(0f, - 90f, 0f);
            }
            */
            if (transform.position.y > spawn.transform.position.y){
                flyDown();
            }
            else if (transform.position.y < spawn.transform.position.y - 0.5f) {
                flyUp();
            }
            
            return;
        }
         
        // If player IS in zone

        direction = (transform.position - player.transform.position).normalized;
            

        // Fly towards player horizontally
        if (direction.x < -.01f) {
            flyLeft();
        }
        else {
            flyRight();
        }

        if (transform.position.y > player.transform.position.y - 0.5f){
            flyDown();
        }
        else if (transform.position.y < player.transform.position.y - 0.5f) {
            flyUp();
        }

        

    }

    void flyLeft() {
        //transform.rotation = Quaternion.Euler(0f, transform.rotation.y + 90f, 0f);
        transform.RotateAround(new Vector3(0f, transform.position.y, 0f), Vector3.up, 20 * Time.deltaTime);
        if (lastLeft == false) childObject.transform.eulerAngles = new Vector3(0, childObject.transform.eulerAngles.y + 180, 0);
        lastLeft = true;
        //Debug.Log("left");
    }

    void flyRight() {
        //transform.rotation = Quaternion.Euler(0f, transform.rotation.y - 90f, 0f);
        transform.RotateAround(new Vector3(0f, transform.position.y, 0f), Vector3.up, -20 * Time.deltaTime);
        if (lastLeft == true) childObject.transform.eulerAngles = new Vector3(0, childObject.transform.eulerAngles.y + 180, 0);
        lastLeft = false;
        //Debug.Log("right");
    }

    void flyUp() {
        rb.AddForce(Vector3.up * 0.4f);
        //Debug.Log("up");
    }

    void flyDown() {
        rb.AddForce(Vector3.up * -0.4f);
        //Debug.Log("down");
    }
}
