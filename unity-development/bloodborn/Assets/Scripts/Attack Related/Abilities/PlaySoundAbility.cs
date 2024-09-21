using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Play Sound", fileName = "Empty Play Sound Ability")]
public class PlaySoundAbility : BaseAbility
{
    [Header("Play Sound Related Settings")]
    [SerializeField] private string clipName;
    [SerializeField] private AudioClip clip;

    public override void Activate()
    {
        base.Activate();

        abilityManager.PlaySound(clip);
    }
}
