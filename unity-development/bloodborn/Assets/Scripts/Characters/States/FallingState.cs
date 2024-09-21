using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Animancer.FSM;

public sealed class FallingState : CharacterState
{
    [SerializeField] private AnimationClip _Falling;

    private void OnEnable()
    {
        Character.Animancer.Play(_Falling);
    }
}
