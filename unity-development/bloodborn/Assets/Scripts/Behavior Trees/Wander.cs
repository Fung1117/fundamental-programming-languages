using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Wander : Action
{
    CharacterController2D controller;

    public override void OnStart()
    {
        controller = GetComponent<CharacterController2D>();
    }

    public override TaskStatus OnUpdate()
    {
        System.Random random = new System.Random();
        if (random.Next(0, 2) == 1)
        {
            if (random.Next(0, 2) == 1)
            {
                controller.Walk(1f);
            }
            else
            {
                controller.Walk(-1f);
            }
        }
        else
        {
            controller.Walk(0);
        }
        return TaskStatus.Success;
    }
}
