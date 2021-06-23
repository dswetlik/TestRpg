using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Active Skill", menuName = "Skill/Active Skill", order = 0)]
public class ActiveSkill : Skill
{

    public enum AttributeType
    {
        weapon,
        health,
        stamina,
        mana
    }

    [SerializeField] AttributeType attributeType;
    [SerializeField] int attributeChange;
    
    [SerializeField] int minDamageModifier;
    [SerializeField] int maxDamageModifier;

    public int GetDamageModifier() { return Random.Range(minDamageModifier, maxDamageModifier + 1); }

    public AttributeType GetAttributeType() { return attributeType; }
    public int GetAttributeChange() { return attributeChange; }
    public int GetMinDamageModifier() { return minDamageModifier; }
    public int GetMaxDamageModifier() { return maxDamageModifier; }
}
