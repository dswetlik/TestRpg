﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack Type", menuName = "Enemy Attack Type", order = 0)]
public class EnemyAttackType : ScriptableObject
{
    public enum AttackType
    {
        none,
        effect,
        heal,
        damage
    }

    public enum AttackAttribute
    {
        none,
        stamina,
        mana
    }

    [SerializeField] new string name;
    [TextArea(3,5)] [SerializeField] string description;
    [SerializeField] AudioClip audioClip;
    [SerializeField] AttackType attackType;
    [SerializeField] AttackAttribute attackAttribute;
    [SerializeField] int damageModifier;
    [SerializeField] int attributeCost;
    [SerializeField] bool hasStatusEffects;
    [SerializeField] List<StatusEffect> statusEffects = new List<StatusEffect>();

    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public AudioClip GetAudioClip() { return audioClip; }
    public AttackType GetAttackType() { return attackType; }
    public AttackAttribute GetAttackAttribute() { return attackAttribute; }
    public int GetDamageModifier() { return Random.Range(0, damageModifier + 1); }
    public int GetAttributeCost() { return attributeCost; }

    public bool HasStatusEffects() { return hasStatusEffects; }
    public List<StatusEffect> GetStatusEffects() { return statusEffects; }

    public int GetMaxDamageModifier() { return damageModifier; }
}
