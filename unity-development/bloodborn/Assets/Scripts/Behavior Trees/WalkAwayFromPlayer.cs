using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Walk away from the player")]
public class WalkAwayFromPlayer : Action
{
    [SerializeField] private float walkUntilRange = 8f;

    Player player;
    Character self;
    CharacterController2D controller;

    public override void OnStart()
    {
        player = Player.player;
        self = GetComponent<Character>();
        controller = GetComponent<CharacterController2D>();
    }
    public override TaskStatus OnUpdate()
    {
        controller.Walk(-Mathf.Sign(player.transform.position.x - self.transform.position.x));
        if (Vector2.Distance(player.transform.position, self.transform.position) >= walkUntilRange)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }

    public override void OnEnd()
    {
        controller.Walk(0f);
    }
}
