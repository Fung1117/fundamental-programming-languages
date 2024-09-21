using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Spawn Expanding Projectile", fileName = "Empty Expanding Projectile Ability")]
public class ExpandingProjectileAbility : BaseAbility
{
    [Header("Projectile Related Settings")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private TargetType target;
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private bool ignoreFacingDireciton;
    [SerializeField] private int amount;
    [SerializeField] private float spawnDelay=0;
    private int degree;
    [SerializeField] private Vector2 positionOffset;

    public enum TargetType
    {
        ALL,
        ENEMY,
        PLAYER
    }

    public override void Activate()
    {
        base.Activate();
        if (spawnDelay > 0)
        {
            GameManager.gameManager.StartCoroutine(spawnProjectiles());
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {

                degree = i * 360 / amount;
                abilityManager.SpawnPrefabs(projectile, true, degree, positionOffset.x, positionOffset.y, ignoreFacingDireciton, baseDamage);
            }
        }

    }
    private IEnumerator spawnProjectiles()
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            degree = i * 360 / amount;
            abilityManager.SpawnPrefabs(projectile, true, degree, positionOffset.x, positionOffset.y, ignoreFacingDireciton, baseDamage);
        }
    }
}
