using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject statusMenu;
    [SerializeField] private GameObject instructionMenu;

    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject bossBar;
    [SerializeField] private GameObject postProcessing;

    public static bool isPaused =false;
    public static bool active_bossbar =false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if  (bossBar.activeSelf == true){
                active_bossbar = true;
            }

            // Debug.Log(isPaused);
            if(isPaused){
                
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        active_bossbar = bossBar.activeSelf;
        postProcessing.SetActive(false);
        healthBar.SetActive(false);
        bossBar.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;

        isPaused = true;
    }

    public void ResumeGame(){

        pauseMenu.SetActive(false);
        statusMenu.SetActive(false);
        instructionMenu.SetActive(false);
        postProcessing.SetActive(true);
        healthBar.SetActive(true);
        bossBar.SetActive(active_bossbar);
        Time.timeScale = 1f;
        isPaused = false;
        AudioListener.pause = false;

    }
    public void getStats() {
        pauseMenu.SetActive(false);
        statusMenu.SetActive(true);
    }
    public void Instruction() {
        pauseMenu.SetActive(false);
        instructionMenu.SetActive(true);
    }
    public void QuitApp() {
        Debug.Log("Application has quit.");

        Application.Quit();
    }
}
