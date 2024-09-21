using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Return true when the player is detected")]
public class PlayerWithinSight : Conditional
{
    [SerializeField] private float detectRange;
    Character self;

    public override void OnStart()
    {
        self = GetComponent<Character>();        
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector2.Distance(self.transform.position, Player.player.transform.position) < detectRange)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
