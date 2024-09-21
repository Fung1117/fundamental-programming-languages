using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject postProcessing;
    [SerializeField] private GameObject bossBar;

    // Update is called once per frame
    void Update()
    {
        if(Player.player.GetStat().bp<=0)
        {
            Player.player.ResetBP();
            PlayerController player = Player.player.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.SoftRespawn();
                bossBar.SetActive(false);
            }
            Player.player.ResetBP();

            Time.timeScale = 0f;
            PauseMenu.isPaused = true;
            AudioListener.pause = true;
            endgame();
        }
    }
    public void endgame(){
        endMenu.SetActive(true);
        postProcessing.SetActive(false);
        Debug.Log(PauseMenu.isPaused);
        Debug.Log(Time.timeScale);

    }
    public void RestartGame(){
        postProcessing.SetActive(true);
        Player.player.ResetBP();
        endMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        AudioListener.pause = false;
    }
}
