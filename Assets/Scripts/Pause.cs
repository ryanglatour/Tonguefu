using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused;

    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape input detected");
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGamne();
            }
        }
    }

    void PauseGamne()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //This only works in standalone builds, doesn't work when testing in unity editor
    public void QuitGame()
    {
        Application.Quit();
    }
}
