using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Animancer.FSM;

public sealed class ActionState : CharacterState
{
    [SerializeField] private ClipTransition _Animation;

    private void OnEnable()
    {
        Character.Animancer.Play(_Animation);
    }

    private void Awake()
    {
        _Animation.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
    }

    public override CharacterStatePriority Priority => CharacterStatePriority.Medium;

    public override bool CanInterruptSelf => true;
}
