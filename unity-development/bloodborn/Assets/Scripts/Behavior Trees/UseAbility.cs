using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Use an ability.")]
public class UseAbility : Action
{
    public GenericAbility ability;

    public override TaskStatus OnUpdate()
    {
        GetComponent<Character>().battleBehavior.UseAbility(ability);

        return TaskStatus.Success;
    }
}
