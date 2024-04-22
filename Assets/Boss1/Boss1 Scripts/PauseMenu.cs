using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausedMenu;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        PausedMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
        
    }

    public void PauseGame(){
        PausedMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame(){
        PausedMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart(){
        SceneManager.LoadScene("Boss1");
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void Win (){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
        isPaused = false;
    }
}
