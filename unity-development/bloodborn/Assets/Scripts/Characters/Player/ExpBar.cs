using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        SetMaxExp(0);
    }

    private void Update()
    {
        SetExp(Player.player.GetStat().exp);
    }


    public void SetMaxExp(int Exp)
    {
        slider.maxValue = Exp;
    }

    public void SetExp(int Exp)
    {
        slider.value = Exp;
    }
}
