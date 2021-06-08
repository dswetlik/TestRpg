using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Active Skill", menuName = "Skill/Active Skill", order = 0)]
public class ActiveSkill : Skill
{

    public enum AttributeType
    {
        health,
        stamina,
        mana
    }

    [SerializeField] AttributeType attributeType;
    [SerializeField] int attributeChange;
    
    [SerializeField] int minDamageModifier;
    [SerializeField] int maxDamageModifier;

    public int GetDamageModifier() { return Random.Range(minDamageModifier, maxDamageModifier + 1); }
}
