using System.Collections;
using UnityEngine;

public class FlowerBoss : Enemy
{
    [Header("For Testing Only")]
    [SerializeField] private float attackInterval;
    
    public override void OnStart()
    {
        StartCoroutine(AttackEveryTwoSecond());
    }

    IEnumerator AttackEveryTwoSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            battleBehavior.UseAbility(abilities[1]);
        }
    }
}
