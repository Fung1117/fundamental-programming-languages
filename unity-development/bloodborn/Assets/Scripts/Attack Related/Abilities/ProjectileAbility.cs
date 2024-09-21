using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Spawn Projectile", fileName = "Empty Projectile Ability")]
public class ProjectileAbility : BaseAbility
{
    [Header("Projectile Related Settings")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private TargetType target;
    [SerializeField] private bool aimAtPlayerOnStart = false;
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private bool ignoreFacingDireciton;
    [SerializeField] private int degree;
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
        abilityManager.SpawnPrefabs(projectile, true, degree, positionOffset.x, positionOffset.y, ignoreFacingDireciton, baseDamage, aimAtPlayerOnStart);
    }
}
