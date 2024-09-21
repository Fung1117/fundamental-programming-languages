using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DoorTriggerController : MonoBehaviour
{
    public string nextMapName;

    // public string nextMapPath;
    public GameObject spawnPoint;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        // print to debug console when player enters the trigger
        // Debug.Log("On trigger");
        if (obj.gameObject.name == "Player")
        {   
            StartCoroutine(LoadScene(obj.gameObject, nextMapName));

            // obj.gameObject.SetActive(true);

        }
    }
    private IEnumerator LoadScene(GameObject player, string nextMapName)
    {
        // AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextMapName, LoadSceneMode.Single);
        // while (!asyncLoad.isDone)
        // {   
        //     yield return null;
        // }

        // hide the player temporarily
        player.SetActive(false);
        // get back to origin
        player.transform.position = new Vector3(0, 0, 0);

        // SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(nextMapName);

        player.SetActive(true);

        // yield return new WaitForSeconds(1);
        // yield return new WaitForSecondsRealtime(0.5f);
        yield return null;
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    // {
    //     Debug.Log("!!!!!");
    //     GameObject player = GameObject.Find("Player");
    //     if (player != null)
    //     {
    //         Debug.Log("&&&&&");
    //         player.SetActive(true);
    //     }
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

}



//idea for portal 
// create a ui for story
// set the StoryUI to false first 
// then true when trigger the portal in the doortrigger script