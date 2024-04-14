using UnityEngine;
using System.Collections;

public class BeeHiveController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        Invoke("Disappear", 10f);
    }

    private void Disappear()
    {
        gameObject.SetActive(true);
    }
}