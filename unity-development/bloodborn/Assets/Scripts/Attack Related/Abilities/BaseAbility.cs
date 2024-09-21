using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseAbility : ScriptableObject
{
    [HideInInspector] public CharacterAbilityBehaviour abilityManager = null;

    public List<BaseAbility> onActivate = new List<BaseAbility>();

    public virtual void Initiate(CharacterAbilityBehaviour _abilityManager)
    {
        abilityManager = _abilityManager;
        // Initiate all abilities in onActivate
        for (int i = 0; i < onActivate.Count; i++)
        {
            onActivate[i].Initiate(_abilityManager);
        }
    }

    public virtual void Activate()
    {
        if (abilityManager == null) return;
        // Activate all abilities in onActivate
        for (int i = 0; i < onActivate.Count; i++)
        {
            onActivate[i].Activate();
        }
    }
}