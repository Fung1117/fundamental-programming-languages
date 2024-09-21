using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Slider slider;

    private void Update()
    {
        SetMaxHealth(Player.player.GetStat().max_bp);
        SetHealth(Player.player.GetStat().bp);
    }


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
