using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Skill", menuName = "Skills/Base Skill", order = 0)]
public class Skill : ScriptableObject
{

    public enum SkillType
    {
        none,
        weapon,
        health,
        stamina,
        mana
    }

    [SerializeField] uint ID;
    [SerializeField] SkillType skillType;
    [SerializeField] Sprite sprite;
    [SerializeField] new string name;
    [TextArea(3,5)]
    [SerializeField] string description;
    [TextArea(3, 5)]
    [SerializeField] string cardDescription;
    [TextArea(3,5)]
    [SerializeField] string actionMessage;
    [SerializeField] bool isActive;
    [SerializeField] int attributeCost;
    [SerializeField] int minDamage, maxDamage;
    [SerializeField] bool hasStatusEffects;
    [SerializeField] List<StatusEffect> statusEffects = new List<StatusEffect>();

    public uint GetID() { return ID; }
    public SkillType GetSkillType() { return skillType; }
    public Sprite GetSprite() { return sprite; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public string GetCardDescription() { return cardDescription; }
    public string GetActionMessage() { return actionMessage; }
    public bool IsActive() { return isActive; }
    public int GetAttributeCost() { return attributeCost; }
    public int GetMinDamage() { return minDamage; }
    public int GetMaxDamage() { return maxDamage; }

    public int GetDamageModifier() { return Random.Range(minDamage, maxDamage + 1); }

    public List<StatusEffect> GetStatusEffects() { return statusEffects; }
    public bool HasStatusEffects() { return hasStatusEffects; }
  
    public void SetActive(bool x) { isActive = x; }

}
