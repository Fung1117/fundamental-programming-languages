using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Statemenu : MonoBehaviour
{
    [SerializeField] GameObject statusMenu;
    [SerializeField] GameObject pauseMenu;
    public TMP_Text StatText;


    public static bool isPaused = false;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.isPaused  == true){
        //     BackGame();
        // }

        StatText.text = "BP:  " + Player.player.GetStat().bp +"/"+ Player.player.GetStat().max_bp+"\n\nATK: " +Player.player.GetStat().atk + "\n\nDEF: " +Player.player.GetStat().def;

    }
    public void BackGame()
    {
        statusMenu.SetActive(false);
        pauseMenu.SetActive(true);

    }
    
}
