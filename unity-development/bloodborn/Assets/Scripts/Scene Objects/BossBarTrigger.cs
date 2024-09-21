using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossBar;
    [SerializeField] private FlowerBoss boss;
    private bool activated = false;
    private bool bossSpawn = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        activated = bossBar.activeSelf;
        if (other.CompareTag("Player") && boss.GetStat().bp>0)
        {
            activated = !activated;
            bossBar.SetActive(activated);
        }
        if (other.CompareTag("Player") && bossSpawn==false && boss!=null && boss.GetStat().bp > 0)
        {
            boss.gameObject.SetActive(true);
        }
    }
}
