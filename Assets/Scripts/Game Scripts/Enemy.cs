using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Normal Enemy", order = 0)]
public class Enemy : ScriptableObject
{

    public enum EnemyType
    {
        normal,
        boss
    }

    [Header("Base Information")]
    [SerializeField] uint id;
    [SerializeField] EnemyType enemyType;
    [SerializeField] new string name;
    [TextArea(3, 5)] [SerializeField] string description;
    

    [Header("Attributes")]
    [SerializeField][Tooltip("Must be an integer.\nX = Min; Y = Max")] Vector2 levelRange;
    [SerializeField] int defense;
    [SerializeField] int strength, agility, intelligence, luck;
    int health, stamina, mana;

    [SerializeField] List<EnemyAttackType> attackTypes;
    
    [Header("Rewards")]
    [SerializeField] float speed;
    [SerializeField] uint expReward;
    [SerializeField] ItemLootTable itemRewards;

    List<StatusEffect> statusEffects = new List<StatusEffect>();

    public void Initialize()
    {
        health = GetMaxHealth();
        mana = GetMaxMana();
        stamina = GetMaxStamina();
    }

    public EnemyType GetEnemyType() { return enemyType; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public uint GetID() { return id; }
    public int GetMinLevel() { return (int)levelRange.x; }
    public int GetMaxLevel() { return (int)levelRange.y; }

    public int GetDefense() { return defense; }

    public int GetStrength() { return strength + (int)GetTotalEffect(StatusEffect.StatusEffectType.strength); }
    public int GetAgility() { return agility + (int)GetTotalEffect(StatusEffect.StatusEffectType.agility); }
    public int GetIntelligence() { return intelligence + (int)GetTotalEffect(StatusEffect.StatusEffectType.intelligence); }
    public int GetLuck() { return luck; }

    public int GetBaseDamage() { return (GetStrength() / 10); }

    public int GetHealth() { return health; } 
    public int GetStamina() { return stamina; }
    public int GetMana() { return mana; }
   
    public int GetMaxHealth() { return GetStrength(); }
    public int GetMaxStamina() { return GetAgility(); }
    public int GetMaxMana() { return GetIntelligence(); }

    public int GetStaminaRegen() { return GetAgility() / 2; }
    public int GetManaRegen() { return GetIntelligence() / 2; }

    public float GetSpeed() { return speed; }

    public uint GetExpReward() { return expReward; }

    public List<EnemyAttackType> GetEnemyAttackTypes() { return attackTypes; }
    public ItemLootTable GetItemRewards() { return itemRewards; }

    public int GetRandomGoldReward() { return itemRewards.GetRandomGold(); }
    public int GetMinGoldReward() { return itemRewards.GetMinGold(); }
    public int GetMaxGoldReward() { return itemRewards.GetMaxGold(); }

    public int GetMaxDamage()
    {
        int maxDamage = 0;
        foreach(EnemyAttackType attack in attackTypes)
        {
            if (attack.GetMaxDamageModifier() > maxDamage)
                maxDamage = attack.GetMaxDamageModifier();
        }
        return maxDamage + GetBaseDamage();
    }

    public void RegenAttributes()
    {
        ChangeStamina(GetStaminaRegen());
        ChangeMana(GetManaRegen());
    }

    public void ChangeHealth(int healthChange)
    {
        health += healthChange;

        if (health > GetMaxHealth()) health = GetMaxHealth();
        if (health < 0) health = 0;
    }

    public void ChangeStamina(int staminaChange)
    {
        stamina += staminaChange;

        if (stamina > GetMaxStamina()) stamina = GetMaxStamina();
        if (stamina < 0) stamina = 0;
    }

    public void ChangeMana(int manaChange)
    {
        mana += manaChange;

        if (mana > GetMaxMana()) mana = GetMaxMana();
        if (mana < 0) mana = 0;
    }

    public bool CheckShouldRest()
    {
        if (attackTypes.TrueForAll(x => (
        x.GetAttackAttribute() == EnemyAttackType.AttackAttribute.stamina && stamina < x.GetAttributeCost()) ||
        x.GetAttackAttribute() == EnemyAttackType.AttackAttribute.mana && mana < x.GetAttributeCost()))
        {
            return true;
        }
        else if (health < (health * 0.5f) &&
            attackTypes.Where(x => x.GetAttackType() == EnemyAttackType.AttackType.heal).Any(x => x.GetAttributeCost() < mana))
        {
            return true;
        }
        return false;
    }

    /*
    public bool CheckShouldDefend()
    {
        if (defense <= 0)
            return false;
        else if(Engine.player.)

    }
    */

    public EnemyAttackType GetAttack()
    {
        int maxDamage = 0;
        EnemyAttackType maxAttack = null;

        foreach (EnemyAttackType attackType in attackTypes)
        {
            if (attackType.GetAttackType() == EnemyAttackType.AttackType.heal)
            {
                if (health < GetMaxHealth() * 0.3f && mana >= attackType.GetAttributeCost())
                    return attackType;
            }
            else
            {
                if (attackType.GetAttackAttribute() == EnemyAttackType.AttackAttribute.stamina)
                {
                    if (stamina >= attackType.GetAttributeCost())
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
                                if (attackType.GetStatusEffects().Any(x => x.GetStatusEffectType() == StatusEffect.StatusEffectType.heal) && health < GetMaxHealth() * 0.5f)
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
                    if (mana >= attackType.GetAttributeCost())
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
                                if (attackType.GetStatusEffects().Any(x => x.GetStatusEffectType() == StatusEffect.StatusEffectType.heal) && health < GetMaxHealth() * 0.5f)
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

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        if(statusEffects.Contains(statusEffect))
            statusEffects.Remove(statusEffect);
    }

    float GetTotalEffect(StatusEffect.StatusEffectType statusEffectType)
    {
        float effectTotal = 0;
        foreach (StatusEffect status in statusEffects)
        {
            if (statusEffectType == status.GetStatusEffectType())
                effectTotal += status.GetStatChange();
        }
        return effectTotal;
    }
}
