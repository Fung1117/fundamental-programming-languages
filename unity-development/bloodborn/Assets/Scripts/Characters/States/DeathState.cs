using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Animancer.FSM;

public sealed class DeathState : CharacterState
{
    [SerializeField] private ClipTransition _Animation;

    private void OnEnable()
    {
        Character.Animancer.Play(_Animation);
    }

    private void Awake()
    {
        _Animation.Events.OnEnd = DisableCharacter;
    }

    void DisableCharacter()
    {
        Character.gameObject.SetActive(false);
    }

    public override CharacterStatePriority Priority => CharacterStatePriority.High;

    public override bool CanInterruptSelf => false;
}
