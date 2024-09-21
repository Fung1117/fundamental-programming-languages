using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public Image cursorImage; 
    public float xOffset = 20f; 
    public float yOffset = -15f; 

    void Update()
    {
        // Set the position of the cursor image to the mouse position plus the x offset
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.x += xOffset;
        mousePosition.y += yOffset;
        cursorImage.transform.position = mousePosition;
    }
}