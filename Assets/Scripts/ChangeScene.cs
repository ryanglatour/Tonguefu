using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    FadeOut fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeOut>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ChangeThatScene()
    {
        fade.Fade();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Boss1");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(ChangeThatScene());
        }
    }
}
