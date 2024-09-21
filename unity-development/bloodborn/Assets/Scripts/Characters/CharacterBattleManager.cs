using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class CharacterBattleManager
{   
    private readonly Character self;
    private CharacterAbilityBehaviour abilityBehaviour;

    public CharacterBattleManager(Character _self, CharacterAbilityBehaviour _abilityBehaviour){
        self = _self;
        abilityBehaviour = _abilityBehaviour;

        // Start checking for passive regen
        self.StartCoroutineInCharacter(PassiveRegen());
    }

    public void UseAbility(int i)
    {
        if (abilityBehaviour != null)
        {
            abilityBehaviour.UseAbility(i);
        }
    }

    public void UseAbility(GenericAbility ability)
    {
        if (self.GetStat().bp <= 0) return;
        self.engager.Engage();
        if (abilityBehaviour != null)
        {
            abilityBehaviour.UseAbility(ability);
        }
    }

    public void UseMelee()
    {
        self.engager.Engage();
        abilityBehaviour.UseMelee();
    }

    // Applying damage to this character
    public int TakeDamage(AttackData incomeAtk)
    {
        //Random random= new Random();
        float crit=1f;
        /*
        int num1 = random.Next(257);
        int num2 = random.Next(257);
        if ((0.5+incomeAtk.GetCritRate())*num1 > num2+10){
           crit=incomeAtk.GetCritBuff();
        }*/
        float dmg = crit * incomeAtk.baseAtk * incomeAtk.GetAttackSource().GetStat().atk/ ( self.GetStat().def);
        return TakeDamage((int)dmg);
    }
    public int TakeDamage(int damage)
    {
        self.SetStat(self.GetStat().AddBloodPoint(-damage));
        self.engager.Engage();
        System.Random random = new System.Random();
        double randomX;
        double randomY;
        if (self.textSpawnOrigin == null)
        { 
            randomX = self.transform.position.x + (random.NextDouble() - 0.5) * self.textDistanceMaxOffset;
            randomY = self.transform.position.y + (random.NextDouble() - 0.5) * self.textDistanceMaxOffset;
        }
        else
        {
            randomX = self.textSpawnOrigin.position.x + (random.NextDouble() - 0.5) * self.textDistanceMaxOffset;
            randomY = self.textSpawnOrigin.position.y + (random.NextDouble() - 0.5) * self.textDistanceMaxOffset;
        }
        if (self.damageTextData == null)
        {
            if (damage < 0)
            {
                DynamicTextManager.CreateText2D(new Vector2((float)randomX, (float)randomY), "+" + (-damage).ToString(), DynamicTextManager.defaultData);
            }
            else
            {
                DynamicTextManager.CreateText2D(new Vector2((float)randomX, (float)randomY), damage.ToString(), DynamicTextManager.defaultData);
            }
        }
        else
        {
            if (damage < 0)
            {
                DynamicTextManager.CreateText2D(new Vector2((float)randomX, (float)randomY), "+" + (-damage).ToString(), self.damageTextData);
            }
            else
            {
                DynamicTextManager.CreateText2D(new Vector2((float)randomX, (float)randomY), damage.ToString(), self.damageTextData);
            }
        }

        if (self.DoCameraShakeOnHit && damage > 0)
        {
            if (self.cameraShakeOnHit != null)
            {
                if (self.GetStat().bp <= 0 && self.cameraShakeOnDeath != null)
                {
                    CameraShakerHandler.Shake(self.cameraShakeOnDeath);
                }
                else
                {
                    CameraShakerHandler.Shake(self.cameraShakeOnHit);
                }
            }
            else
            {
                Debug.LogWarning("You tried to do a camera shake but you haven't assigned the shake data!");
            }
        }
        
        if (damage > 0)
        {
            DamageGlow();
        }

        if (self.GetStat().bp <= 0)
        {
            Die();
        }
        
        return damage;
    }

    public int RemoveBP(int damage)
    {
        self.SetStat(self.GetStat().AddBloodPoint(-damage));
        return damage;
    }

    public void Die()
    {
        self.OnDeath();
        self.brain.Die();
    }

    // Naturally regenerate health when the player is not engaged in any battle
    public IEnumerator PassiveRegen()
    {   
        while(self.GetStat().bp > 0)
        {
            if(!self.engager.isEngaged()){
                int heal_amount;
                if (self.GetStat().bp<self.GetStat().max_bp)
                {
                    if(self.GetStat().max_bp-self.GetStat().bp>self.GetStat().regenAmount){
                        heal_amount=self.GetStat().regenAmount;
                    }
                    else{
                    heal_amount=self.GetStat().max_bp-self.GetStat().bp;
                    }
                    self.battleBehavior.TakeDamage(-heal_amount);
                }
                yield return new WaitForSeconds(self.GetStat().regenSpeed);
            }
            else{
                yield return new WaitForSeconds(5f);
            }
            
        }
        
    }
    public IEnumerator DamageGlow()
    {
        SpriteRenderer sprite = self.gameObject.GetComponentInChildren<SpriteRenderer>();
        sprite.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(5f);
        sprite.color = new Color(1, 1, 1, 1);
    }
}
