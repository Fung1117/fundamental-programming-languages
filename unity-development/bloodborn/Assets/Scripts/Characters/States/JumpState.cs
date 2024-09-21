using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class JumpState : CharacterState
{
    [SerializeField] private AnimationClip _ground_jump;
    [SerializeField] private AnimationClip _air_jump;

    private void OnEnable()
    {
        if (Character.brain.isGrounded)
        {
            Character.Animancer.Play(_ground_jump);
        }
        else
        {
            Character.Animancer.Play(_air_jump);
        }
        Character.brain.vSpeed = 0f;
        Character.brain.isGrounded = false;
        Character.brain.isOnWall = false;
        Character.brain.isOnLadder = false;
        Character.brain.isDashing = false;
    }

    private void LateUpdate()
    {
        if (Character.brain.isGrounded || Character.brain.vSpeed < 0 || Character.brain.isDashing || Character.brain.isOnWall || Character.brain.isOnLadder)
        {
            Character.StateMachine.ForceSetDefaultState();
        }
    }

    public override CharacterStatePriority Priority => CharacterStatePriority.Medium;

    public override bool CanInterruptSelf => true;
}
