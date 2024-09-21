using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Stop moving")]
public class StopMovement : Action
{
    CharacterController2D controller;

    public override void OnStart()
    {
        controller = GetComponent<CharacterController2D>();
    }

    public override TaskStatus OnUpdate()
    {
        controller.Walk(0);
        return TaskStatus.Success;
    }
}
