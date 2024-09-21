using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    [SerializeField] GameObject instructionMenu;
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.isPaused == true){
        //     BackGame();
        // }
        
    }
    public void BackGame()
    {
        instructionMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
