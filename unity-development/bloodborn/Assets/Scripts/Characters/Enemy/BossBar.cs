using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private FlowerBoss boss;
    [SerializeField] private GameObject bossBar;
    [SerializeField] private GameObject theEnd;
    //[SerializeField] private GameObject explosion;

    public AudioClip deathSound;
    private AudioSource source;
    private void Update()
    {
        if (boss.GetStat().bp <= 0)
        {
            source = boss.gameObject.GetComponent<AudioSource>();
            source.PlayOneShot(deathSound);
            bossBar.SetActive(false);
            Invoke("ActivateGameObject", 2f);

        }
        SetMaxHealth(boss.GetStat().max_bp);
        SetHealth(boss.GetStat().bp);
    }


    void ActivateGameObject()
    {
        theEnd.SetActive(true);
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
