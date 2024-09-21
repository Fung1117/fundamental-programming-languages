using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : Character
{
    public static Player player;

    [Header("Melee Ability")]
    [SerializeField] private GenericAbility meleeAbility;

    public override void OnAwake() {
        if (Player.player != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Player.player = this;
            GameManager.player = this;
        }
        abilityBehavior.AddMelee(meleeAbility);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Melee Attack
        if (Input.GetKeyDown(KeyCode.J))
        {
            battleBehavior.UseMelee();
        }
        // Using abilities
        if (Input.GetKeyDown(KeyCode.U))
        {
            battleBehavior.UseAbility(0);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            battleBehavior.UseAbility(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            battleBehavior.UseAbility(2);
        }
    }
}