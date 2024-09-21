using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static Player player;
    public static GameManager gameManager;

    [SerializeField] private CinemachineVirtualCamera cinemachine;

    private void Awake()
    {
        if (gameManager != null)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

    private void Start()
    {
        cinemachine.LookAt = player.transform;
        cinemachine.Follow = player.transform;
    }
}
