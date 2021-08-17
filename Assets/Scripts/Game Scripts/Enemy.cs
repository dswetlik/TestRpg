using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies", order = 0)]
public class Enemy : ScriptableObject
{
    [SerializeField] Material material;

    [SerializeField] new string name;
    [SerializeField] string description;
    [SerializeField] uint id;
    [SerializeField] int minDamage, maxDamage;
    [SerializeField] List<EnemyAttackType> attackTypes;
    [SerializeField] int health, maxHealth, stamina, maxStamina, stamRegen, mana, maxMana, manaRegen, goldReward;
    [SerializeField] float speed;
    [SerializeField] uint expReward;
    [SerializeField] ItemLootTable itemRewards;
 
    public Material GetMaterial() { return material; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public uint GetID() { return id; }
    public int GetMaxDamage() { return maxDamage; }
    public int GetMinDamage() { return minDamage; }
    public int GetBaseDamage() { return Random.Range(minDamage, maxDamage + 1); }
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

        Debug.Log(attackTypes.Count);

        foreach (EnemyAttackType attackType in attackTypes)
        {
            Debug.Log(attackType.GetName());
            if (attackType.GetAttackType() == EnemyAttackType.AttackType.heal)
            {
                if (health < maxHealth * 0.25f && mana >= attackType.GetManaCost())
                    return attackType;
            }
            else
            {
                if (attackType.GetAttackAttribute() == EnemyAttackType.AttackAttribute.stamina)
                {
                    if (stamina >= attackType.GetStaminaCost())
                        if (maxDamage <= attackType.GetDamageModifier())
                        {
                            maxDamage = attackType.GetDamageModifier();
                            maxAttack = attackType;
                        }
                }
                else if (attackType.GetAttackAttribute() == EnemyAttackType.AttackAttribute.mana)
                {
                    if (mana >= attackType.GetManaCost())
                        if (maxDamage <= attackType.GetDamageModifier())
                        {
                            maxDamage = attackType.GetDamageModifier();
                            maxAttack = attackType;
                        }
                }
            }
        }
        return maxAttack;
    }

}
