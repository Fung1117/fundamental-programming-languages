using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    [field: SerializeField] public int baseAtk { get; private set; }
    
    //Not implemented
    [field: SerializeField] public float critRate { get; private set; }
    [field: SerializeField] public float critBuff { get; private set; }
    private Character attackSource;

    public AttackData(int _baseAtk, Character _attackSource)
    {
        baseAtk = _baseAtk;
        attackSource = _attackSource;
    }
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

    public Character GetAttackSource()
    {
        return(attackSource);
    }
    public Character SetAttackSource(Character _attackSource)
    {
        attackSource = _attackSource;
        return(attackSource);
    }
}

