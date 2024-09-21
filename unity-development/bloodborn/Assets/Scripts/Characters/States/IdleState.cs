using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;


public sealed class IdleState : CharacterState
{
    [SerializeField] private AnimationClip _Animation;

    private void OnEnable()
    {
        Character.Animancer.Play(_Animation);
    }
}
