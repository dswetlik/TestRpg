using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack Type", menuName = "Enemy Attack Type", order = 0)]
public class EnemyAttackType : ScriptableObject
{
    public enum AttackType
    {
        bite,
        scratch,
        slash,
        stab,
        bash,
        burn,
        shock,
        freeze,
        heal
    }

    public enum AttackAttribute
    {
        none,
        stamina,
        mana
    }

    [SerializeField] new string name;
    [SerializeField] AttackType attackType;
    [SerializeField] AttackAttribute attackAttribute;
    [SerializeField] int minDamageModifier;
    [SerializeField] int maxDamageModifier;
    [SerializeField] int staminaCost;
    [SerializeField] int manaCost;

    public string GetName() { return name; }
    public AttackType GetAttackType() { return attackType; }
    public AttackAttribute GetAttackAttribute() { return attackAttribute; }
    public int GetDamageModifier() { return Random.Range(minDamageModifier, maxDamageModifier + 1); }
    public int GetMinDamageModifier() { return minDamageModifier; }
    public int GetMaxDamageModifier() { return maxDamageModifier; }

    public int GetStaminaCost() { return staminaCost; }
    public int GetManaCost() { return manaCost; }
}
