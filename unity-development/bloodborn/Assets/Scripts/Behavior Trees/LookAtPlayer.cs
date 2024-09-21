using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Face the Player")]
public class LookAtPlayer : Action
{
    Player player;
    Character self;
    CharacterController2D controller;
    bool framePast = false;

    public override void OnStart()
    {
        player = Player.player;
        self = GetComponent<Character>();
        controller = GetComponent<CharacterController2D>();
        framePast = false;
    }
    public override TaskStatus OnUpdate()
    {
        if (framePast) return TaskStatus.Success;
        controller.Walk(Mathf.Sign(player.transform.position.x - self.transform.position.x));
        framePast = true;
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        controller.Walk(0);
    }
}
