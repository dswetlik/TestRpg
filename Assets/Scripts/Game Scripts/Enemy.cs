using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Normal Enemy", order = 0)]
public class Enemy : ScriptableObject
{
    
    public enum EnemyType
    {
        normal,
        boss
    }

    [SerializeField] EnemyType enemyType;
    [SerializeField] new string name;
    [TextArea(3,5)][SerializeField] string description;
    [SerializeField] uint id;
    [SerializeField] int minLevel, maxLevel;
    [SerializeField] int minDamage, maxDamage;
    [SerializeField] int defense;
    [SerializeField] List<EnemyAttackType> attackTypes;
    [SerializeField] int health, maxHealth, stamina, maxStamina, stamRegen, mana, maxMana, manaRegen, goldReward;
    [SerializeField] float speed;
    [SerializeField] uint expReward;
    [SerializeField] ItemLootTable itemRewards;
    [SerializeField] List<StatusEffect> statusEffects = new List<StatusEffect>();

    public EnemyType GetEnemyType() { return enemyType; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public uint GetID() { return id; }
    public int GetMinLevel() { return minLevel; }
    public int GetMaxLevel() { return maxLevel; }
    public int GetMaxDamage() { return maxDamage; }
    public int GetMinDamage() { return minDamage; }
    public int GetDefense() { return defense; }
    public int GetBaseDamage() { return UnityEngine.Random.Range(minDamage, maxDamage + 1); }
    public uint GetExpReward() { return expReward; }
    public int GetGoldReward() { return goldReward; }
    public int GetHealth() { return health; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetStamina() { return stamina; }
    public int GetMaxStamina() { return maxStamina; }
    public int GetStaminaRegen() { return stamRegen; }
    public int GetMana() { return mana; }
    public int GetMaxMana() { return maxMana; }
    public int GetManaRegen() { return manaRegen; }
    public float GetSpeed() { return speed; }
    public List<EnemyAttackType> GetEnemyAttackTypes() { return attackTypes; }
    public ItemLootTable GetItemRewards() { return itemRewards; }

    public void RegenAttributes()
    {
        stamina += stamRegen;
        mana += manaRegen;

        if (stamina > maxStamina) stamina = maxStamina;
        if (mana > maxMana) mana = maxMana;
    }

    public void ChangeHealth(int healthChange)
    {
        health += healthChange;

        if (health > maxHealth) health = maxHealth;
        if (health < 0) health = 0;
    }

    public void ChangeStamina(int staminaChange)
    {
        stamina += staminaChange;

        if (stamina > maxStamina) stamina = maxStamina;
        if (stamina < 0) stamina = 0;
    }

    public void ChangeMana(int manaChange)
    {
        mana += manaChange;

        if (mana > maxMana) mana = maxMana;
        if (mana < 0) mana = 0;
    }

    public EnemyAttackType GetAttack()
    {
        int maxDamage = 0;
        EnemyAttackType maxAttack = null;

        foreach (EnemyAttackType attackType in attackTypes)
        {
            if (attackType.GetAttackType() == EnemyAttackType.AttackType.heal)
            {
                if (health < maxHealth * 0.3f && mana >= attackType.GetManaCost())
                    return attackType;
            }
            else
            {
                if (attackType.GetAttackAttribute() == EnemyAttackType.AttackAttribute.stamina)
                {
                    if (stamina >= attackType.GetStaminaCost())
                    {
                        if (attackType.GetAttackType() == EnemyAttackType.AttackType.damage)
                        {
                            if (maxDamage <= attackType.GetDamageModifier())
                            {
                                maxDamage = attackType.GetDamageModifier();
                                maxAttack = attackType;
                            }
                        }
                        else if (attackType.GetAttackType() == EnemyAttackType.AttackType.effect)
                        {
                            if(!statusEffects.Any(x => attackType.GetStatusEffects().Any(y => (x.GetName() == y.GetName() && !x.IsNegative() && !y.IsNegative()))))
                            {
                                if (attackType.GetStatusEffects().Any(x => x.GetStatusEffectType() == StatusEffect.StatusEffectType.heal) && health < maxHealth * 0.5f)
                                {
                                    if (UnityEngine.Random.Range(0, 2) == 0)
                                        return attackType;
                                }
                                else
                                {
                                    if (UnityEngine.Random.Range(0, 4) == 0)
                                        return attackType;
                                }
                                    
                            }
                        }
                    }
                }
                else if (attackType.GetAttackAttribute() == EnemyAttackType.AttackAttribute.mana)
                {
                    if (mana >= attackType.GetManaCost())
                    {
                        if (attackType.GetAttackType() == EnemyAttackType.AttackType.damage)
                        {
                            if (maxDamage <= attackType.GetDamageModifier())
                            {
                                maxDamage = attackType.GetDamageModifier();
                                maxAttack = attackType;
                            }
                        }
                        else if (attackType.GetAttackType() == EnemyAttackType.AttackType.effect)
                        {
                            if (!statusEffects.Any(x => attackType.GetStatusEffects().Any(y => (x.GetName() == y.GetName() && !x.IsNegative() && !y.IsNegative()))))
                            {
                                if (attackType.GetStatusEffects().Any(x => x.GetStatusEffectType() == StatusEffect.StatusEffectType.heal) && health < maxHealth * 0.5f)
                                {
                                    if (UnityEngine.Random.Range(0, 2) == 0)
                                        return attackType;
                                }
                                else
                                {
                                    if (UnityEngine.Random.Range(0, 4) == 0)
                                        return attackType;
                                }

                            }
                        }
                    }
                }
            }
        }
        return maxAttack;
    }

    public List<StatusEffect> GetStatusEffects() { return statusEffects; }

    public void AddStatusEffect(StatusEffect statusEffect) { statusEffects.Add(statusEffect); }

    public void DecrementStatusEffectTurn()
    {
        if (statusEffects.Count > 0)
            foreach (StatusEffect statusEffect in statusEffects.ToList<StatusEffect>())
            {
                statusEffect.DecrementTurnCount();
                if (statusEffect.GetTurnAmount() < 1)
                    RemoveStatusEffect(statusEffect);
            }
    }

    public void RemoveStatusEffect(StatusEffect statusEffect) { statusEffects.Remove(statusEffect); }

}
