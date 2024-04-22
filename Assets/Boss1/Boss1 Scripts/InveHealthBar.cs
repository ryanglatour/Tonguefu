using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InveHealthBar : MonoBehaviour
{
    public Slider slider;

    // Function to toggle the visibility of the slider
    public void SetVisibility(bool isVisible)
    {
        slider.gameObject.SetActive(isVisible);
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health; 
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}

