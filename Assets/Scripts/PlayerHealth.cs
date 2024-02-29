using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxhealth = 10;
    public Sprite heart;
    public Image[] hearts;
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Update()
    {
        for(int i = 0; i < maxhealth; i++)
        {
            if(i>=health)
                hearts[i].enabled = false;
            else
                hearts[i].enabled = true;
        }

        if(health == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
