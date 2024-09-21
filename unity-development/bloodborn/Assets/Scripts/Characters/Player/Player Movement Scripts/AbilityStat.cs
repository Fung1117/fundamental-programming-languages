using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStat 
{
    [field: SerializeField] public int baseAtk { get; private set; }
    [field: SerializeField] public float critRate { get; private set; }
    [field: SerializeField] public float critBuff { get; private set; }

    [field: SerializeField] public float coolDown { get; private set; }
    [field: SerializeField] public float travelSpeed { get; private set; }

    public int SetBaseAtk(int _baseAtk)
    {
        baseAtk = _baseAtk;
        return (baseAtk);
    }
    public float SetCritRate(float _critRate)
    {
        critRate = _critRate;
        return (critRate);
    }

    public float SetCritBuff(float _critBuff)
    {
        critBuff = _critBuff;
        return (critBuff);
    }


    
}
