using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(CharacterSoundManager))]
public class CharacterAbilityBehaviour : MonoBehaviour
{
    // This is the list of abilities that the current character owns.
    [HideInInspector] public List<GenericAbility> abilities;
    [SerializeField] private readonly int maxNoOfAbilities = 3;
    private List<float> abilityLastUsed = new List<float>();
    private GenericAbility melee;
    private float meleeLastUsed;

    private void Start()
    {
        for (int i = 0; i < maxNoOfAbilities; i++)
        {
            abilityLastUsed.Add(Time.time);
            meleeLastUsed = Time.time;
        }
    }

    #region Using Ability
    public void UseAbility(int abilityID)
    {
        if (abilityID + 1 > abilities.Count) { return; }
        if (Time.time - abilityLastUsed[abilityID] < abilities[abilityID].cooldown) return;
        if(PauseMenu.isPaused) return;
        if (abilities[abilityID].hasCoolDown) abilityLastUsed[abilityID] = Time.time;
        UseAbility(abilities[abilityID]);
    }

    public void UseAbility(GenericAbility ability)
    {
        this.gameObject.GetComponent<Character>().battleBehavior.RemoveBP(ability.bloodPointRequired);
        ability.Initiate(this);
        ability.Activate();
        GetComponent<Character>().brain.UseAbility(ability);
    }

    public void UseMelee()
    {
        if (Time.time - meleeLastUsed < melee.cooldown) return;
        if (PauseMenu.isPaused) return;
        if (melee.hasCoolDown) meleeLastUsed = Time.time;
        UseAbility(melee);
    }

    public bool IsInCooldown(int abilityID)
    {
        return Time.time - abilityLastUsed[abilityID] < abilities[abilityID].cooldown;
    }

    public float TimeUntilCooldownEnd(int abilityID)
    {
        return Mathf.Max(0f, abilities[abilityID].cooldown - (Time.time - abilityLastUsed[abilityID]));
    }

    public float TimeUntilMeleeCooldownEnd()
    {
        return Mathf.Max(0f, melee.cooldown - (Time.time - meleeLastUsed));
    }
    #endregion

    #region Equipping Ability
    public void AddAbility(GenericAbility ability)
    {
        if (abilities.Count > maxNoOfAbilities)
        {
            Debug.LogWarning("You don't have more space for additional abilities!");
            return;
        }
        abilities.Add(ability);
        ability.Initiate(this);
    }

    public void AddMelee(GenericAbility ability)
    {
        melee = ability;
        ability.Initiate(this);
    }

    public void RemoveAbility(GenericAbility ability)
    {
        abilities.Remove(ability);
    }
    #endregion

    #region Ability Behavior
    public void SpawnPrefabs(GameObject prefab, bool followCharacterDirection, float rotationOffset = 0, float offsetX = 0, float offsetY = 0, bool ignoreFacingDirection = false, int baseDamage = 0, bool aimAtPlayerOnStart = false)
    {
        Vector2 position = gameObject.transform.position;
        position = new Vector2(position.x + offsetX, position.y + offsetY);
        float rotation = 0f;
        if (aimAtPlayerOnStart)
        {
            float floaty = Player.player.target.position.y - position.y;
            float floatx = Player.player.target.position.x - position.x;
            rotation = Mathf.Atan2(floaty, floatx) * Mathf.Rad2Deg;
        }
        else if (!GetComponent<CharacterBrain>().isFacingRight && !ignoreFacingDirection)
        {
            rotation = 180f;
            offsetX *= -1;
        }
        rotation += rotationOffset;
        Projectile bullet = Instantiate(prefab, position, Quaternion.Euler(0, 0, rotation)).GetComponent<Projectile>();
        AttackData attackData = new AttackData(baseDamage, GetAttacker());
        bullet.Initiate(attackData);
    }

    public void PlaySound(string soundName)
    {
        GetComponent<CharacterSoundManager>().PlaySound(soundName);
    }
    
    public void PlaySound(AudioClip clip)
    {
        GetComponent<CharacterSoundManager>().PlaySound(clip);
    }

    private Character GetAttacker()
    {
        return (this.gameObject.GetComponent<Character>());
    }
    #endregion
}