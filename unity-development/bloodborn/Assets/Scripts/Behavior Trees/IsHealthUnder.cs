using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsHealthUnder : Conditional
{
    [SerializeField] private int BP;
    [SerializeField] private bool underAndEqual = true;

    Character character;

    public override void OnStart()
    {
        character = GetComponent<Character>();
    }

    public override TaskStatus OnUpdate()
    {
        if (underAndEqual)
        {
            if (character.GetStat().bp <= BP) return TaskStatus.Success;
            return TaskStatus.Failure;
        }
        else
        {
            if (character.GetStat().bp < BP) return TaskStatus.Success;
            return TaskStatus.Failure;
        }
    }
}
