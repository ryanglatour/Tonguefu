using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxhealth = 10;
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
