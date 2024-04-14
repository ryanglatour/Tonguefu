using UnityEngine;

public class DisappearAfterDelay : MonoBehaviour
{
    private void Start()
    {
        Invoke("Disappear", 6.2f);
    }

    private void Disappear()
    {
        gameObject.SetActive(false);
    }
}

