using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//idea for portal 
// create a ui for story
// set the StoryUI to false first 
// then true when trigger the portal in the doortrigger script

public class PlotMenu : MonoBehaviour
{
    private int num_click = 0;

    public Image icon; 
    public Sprite king;
    public Sprite princess_1;
    public Sprite princess_2;
    public Sprite princess_3;
    public Sprite princess_4;
    public Sprite princess_5;

    public Animator animator;
    public TMP_Text Name;
    public TMP_Text Dialogue;
    public TMP_Text Instruction;

    private ArrayList names = new ArrayList();



    // Start is called before the first frame update
    void Start()
    {


        names.Add("??? :");
        names.Add("King:");
        names.Add("Princess:");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeText();
            num_click++;
            Instruction.text = "";
        }
    }

    public void ChangeText()
    {
        //Appear/Hide
        if (num_click == 1 || num_click == 8)
        {
            animator.SetBool("HaveDialogue", true);
        }
        else if (num_click == 6)
        {
            animator.SetBool("HaveDialogue", false);
        }


        //Change icon
        if (num_click <2)
        {
            icon.sprite = king;
        }
        else if (num_click == 7)
        {
            icon.sprite = princess_1;
        }
        else if (num_click == 12)
        {
            icon.sprite = princess_2;
        }
        else if (num_click == 13)
        {
            icon.sprite = princess_3;
        }
        else if (num_click == 14)
        {
            icon.sprite = princess_4;
        }
        else if (num_click == 16)
        {
            icon.sprite = princess_5;
        }


        //Change name
        if (num_click == 7)
        {
            Name.text = "";
        }
        else if (num_click == 1 || num_click == 8)
        {
            Name.text = (string)names[0];
        }
        else if (num_click > 2 && num_click < 6)
        {
            Name.text = (string)names[1];
        }
        else if (num_click > 9 && num_click < 13)
        {
            Name.text = (string)names[2];
        }


        
        //Change dialogue
        if (num_click == 1 || num_click == 7)
        {
            StopAllCoroutines();
            Dialogue.text = "";
        }
        else if (num_click == 2)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("Welcome, our Hero. I am King Arthur, the ruler of this Kingdom."));

        }
        else if (num_click == 3)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("We have summoned you to save our kingdom."));
        }
        else if (num_click == 4)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("You are our last hope to defeat the devil."));
        }
        else if (num_click == 5)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("But first, my daughter will help you discover your full potential"));
        }
        else if (num_click == 9)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("Greetings. I am Princess Diana"));
        }
        else if (num_click == 10)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("Allow me to analyze your status"));
        }
        else if (num_click == 11)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("Your HP is 100 ... Atk is 10 ... Def is 10 ..."));
        }
        else if (num_click == 12)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("Your physical strength is truly impressive!"));
        }
        else if (num_click == 13)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("Now let's take a look at your magic potential."));
        }
        else if (num_click == 14)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("What?!"));
        }
        else if (num_click == 15)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("You have 0 MP?! And your magic element is ... ... ... BLOOD?!"));
        }
        else if (num_click == 16)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("You are no Hero!"));
        }
        else if (num_click == 17)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("A real Hero wields Light magic blessed by God."));
        }

        else if (num_click == 18)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("Not someone who was tempted by the devil to use the cursed blood magic."));
        }
        else if (num_click == 19)
        {
            Dialogue.text = "";
            StopAllCoroutines();
            StartCoroutine(DisplayDialogue("I order you to leave this kingdom at once!"));
        }
        

        //Change scene
        if (num_click == 20)
        {
            SceneManager.LoadScene("Level 1");
        }

        
    }
    public IEnumerator DisplayDialogue(string txt)
    {
        Dialogue.text = "";
        foreach (char letter in txt.ToCharArray())
        {
            Dialogue.text += letter;
            yield return null;
        }
    }

}
