using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Walk until it is near the player")]
public class WalkNearPlayer : Action
{
    [SerializeField] private float successDetectionRange = 2f;
    [SerializeField] private float failureDetectionRange = 10f;
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
        controller.Walk(Mathf.Sign(player.transform.position.x - self.transform.position.x));
        if (Vector2.Distance(player.transform.position, self.transform.position) <= successDetectionRange) return TaskStatus.Success;
        if (Mathf.Abs(player.transform.position.x - self.transform.position.x) < 0.2f) return TaskStatus.Success;
        if (Vector2.Distance(player.transform.position, self.transform.position) > failureDetectionRange) return TaskStatus.Failure;
            return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        controller.Walk(0f);
    }
}
