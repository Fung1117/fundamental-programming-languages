using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterStatData
{
    //Stat
    [field: SerializeField] public int max_bp {get; private set;}
    [field: SerializeField] public int bp {get; private set;}
    [field: SerializeField] public int atk {get; private set;}
    [field: SerializeField] public int def {get; private set;}

    //Level
    [field: SerializeField] public int level {get; private set;}
    [field: SerializeField] public int exp {get; private set;}

    //Regen
    [field: SerializeField] public float regenStartTime {get; private set;}
    [field: SerializeField] public float regenSpeed {get; private set;}
    [field: SerializeField] public int regenAmount {get; private set;}

    public CharacterStatData(){
        max_bp=10;
        bp=10;
        atk=10;
        def=10;
        level=1;
        exp=0;
        regenStartTime=3.5f;
        regenSpeed=1f;
        regenAmount=1;
    }

    public CharacterStatData SetMaximumBloodPoint(int x)
    {
        max_bp=x;
        return(this);
    }
    public CharacterStatData AddMaximumBloodPoint(int x)
    {
        max_bp+=x;
        return(this);
    }
    
    public CharacterStatData SetBloodPoint(int x)
    {
        bp=x;
        return(this);
    }
    public CharacterStatData AddBloodPoint(int x)
    {
        bp+=x;
        return(this);
    }

    public int SetAtk(int x)
    {
        atk=x;
        return(atk);
    }

    public int SetDef(int x)
    {
        def=x;
        return(def);
    }

    public int SetLevel(int x)
    {
        level=x;
        return(level);
    }
    public int SetExp(int x)
    {
        exp = x;
        return (exp);
    }

    public float SetRegenSpeed(float x)
    {
        regenSpeed=x;
        return(regenSpeed);
    }

    public float SetRegenStartTime(float x)
    {
        regenStartTime=x;
        return(regenStartTime);
    }
    public int SetRegenAmount(int x)
    {
        regenAmount = x;
        return (regenAmount);
    }
}
