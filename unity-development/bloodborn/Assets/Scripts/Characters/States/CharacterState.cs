using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer.FSM;
using Animancer;

[System.Serializable]
public abstract class CharacterState : StateBehaviour
{
    [System.Serializable]
    public class StateMachine : StateMachine<CharacterState>.WithDefault { }

    [SerializeField]
    private Character _Character;
    public Character Character => _Character;

    public enum CharacterStatePriority
    {
        High = 2,
        Medium = 1,
        Low = 0
    }

    #if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        gameObject.GetComponentInParentOrChildren(ref _Character);
    }
    #endif

    public virtual CharacterStatePriority Priority => CharacterStatePriority.Low;

    public virtual bool CanInterruptSelf => false;

    public override bool CanExitState
    {
        get
        {
            // There are several different ways of accessing the state change details:
            // var nextState = StateChange<CharacterState>.NextState;
            // var nextState = this.GetNextState();
            var nextState = _Character.StateMachine.NextState;
            if (nextState == this)
                return CanInterruptSelf;
            else if (Priority == CharacterStatePriority.Low)
                return true;
            else
                return nextState.Priority > Priority;
        }
    }
}