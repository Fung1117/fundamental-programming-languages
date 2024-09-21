using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Animancer.FSM;

public class UseAbilityState : CharacterState
{
    [HideInInspector] public ClipTransition clip;

    public override CharacterStatePriority Priority => CharacterStatePriority.High;

    private void OnEnable()
    {
        //clip.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
        AnimancerState _state = Character.Animancer.Play(clip);
        _state.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
    }
}
