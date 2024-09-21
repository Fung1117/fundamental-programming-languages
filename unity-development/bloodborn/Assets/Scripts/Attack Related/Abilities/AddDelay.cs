using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Abilities/Add Delay", fileName = "Empty Add Delay Ability")]
public class AddDelay : BaseAbility
{
    [Header("Add Delay Related Settings")]
    [SerializeField] private float delayTime;
    [SerializeField] public List<BaseAbility> onDelayEnd = new List<BaseAbility>();

    public override void Initiate(CharacterAbilityBehaviour _abilityManager)
    {
        base.Initiate(_abilityManager);
        for (int i = 0; i < onDelayEnd.Count; i++)
        {
            onDelayEnd[i].Initiate(_abilityManager);
        }
    }

    public override void Activate()
    {
        base.Activate();
        GameManager.gameManager.StartCoroutine(ActivateWithDelay());
    }

    private IEnumerator ActivateWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        for (int i = 0; i < onDelayEnd.Count; i++)
        {
            onDelayEnd[i].Activate();
        }
        yield break;
    }
}
