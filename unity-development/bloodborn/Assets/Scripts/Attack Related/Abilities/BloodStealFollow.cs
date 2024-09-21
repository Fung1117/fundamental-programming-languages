using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStealFollow : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Player.player.gameObject.GetComponent<CharacterController2D>().TotalSpeed * Time.deltaTime);
    }
}
