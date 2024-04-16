using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private string sceneName = "Level3"; // Name of the scene to load

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    //This only works in standalone builds, doesn't work when testing in unity editor
    public void QuitGame()
    {
        Application.Quit();
    }

}
