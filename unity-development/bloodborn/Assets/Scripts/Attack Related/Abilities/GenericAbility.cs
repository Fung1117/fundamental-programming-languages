using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

[CreateAssetMenu(menuName = "Abilities/Generic Ability", fileName = "Empty Ability")]
public class GenericAbility : BaseAbility
{
    [Header("Basic Information")]
    public string abilityName = "Ability Name";
    public Sprite abilityIcon = null;
    public bool isActiveAbility = true;
    public int bloodPointRequired = 0;

    [Header("Cooldown")]
    public bool hasCoolDown = false;
    public float cooldown = 5f;

    [Header("Animation")]
    public ClipTransition clip;
}
