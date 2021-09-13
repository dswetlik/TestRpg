using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Skill", menuName = "Skill/Passive Skill", order = 1)]
public class PassiveSkill : Skill
{ 

    public enum AttributeType
    {
        speed, //
        defense, //
        dodge,
        weaponDamage,//
        skillDamage,//
        manaDamage,//
        carryWeight, //
        maxHealth, //
        maxStamina, //
        maxMana, //
        healthRegen,//
        staminaRegen, //
        manaRegen //
    }

    [SerializeField] AttributeType attributeType;

    [SerializeField] bool isPercentage;
    [SerializeField] float attributeChange;

    public AttributeType GetAttributeType() { return attributeType; }
    
    public bool IsPercentage() { return isPercentage; }
    public float GetAttributeChange() { return attributeChange; }

}
