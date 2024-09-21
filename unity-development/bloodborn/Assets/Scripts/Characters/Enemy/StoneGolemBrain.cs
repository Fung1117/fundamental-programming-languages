using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class StoneGolemBrain : CharacterBrain
{
    [Header("Weakened Animation")]
    [SerializeField] private CharacterState weakenedIdle;
    [SerializeField] private CharacterState weakenedWalk;
    [SerializeField] private CharacterState weakenedFall;
    [SerializeField] private CharacterState weakenedDeath;
    [SerializeField] private CharacterState _weakenedToReinforced;

    private CharacterState reinforcedIdle;
    private CharacterState reinforcedWalk;
    private CharacterState reinforcedFall;
    private CharacterState reinforcedDeath;

    public override void onStart()
    {
        base.onStart();
        reinforcedIdle = _Idle;
        reinforcedWalk = _Move;
        reinforcedFall = _Fall;
        reinforcedDeath = _Death;
    }

    public void Weaken()
    {
        _Character.StateMachine.ForceSetState(_Death);
        _Character.StateMachine.DefaultState = weakenedIdle;
        _Death = weakenedDeath;
        _Move = weakenedWalk;
        _Fall = weakenedFall;
        _Idle = weakenedIdle;
    }

    public void Reinforce()
    {
        _Character.StateMachine.ForceSetState(_weakenedToReinforced);
        _Character.StateMachine.DefaultState = reinforcedIdle;
        _Death = reinforcedDeath;
        _Move = reinforcedWalk;
        _Fall = reinforcedFall;
        _Idle = reinforcedIdle;
        _Character.ResetBP();
    }
}
