using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{

    public enum SkillType
    {
        active,
        passive
    }

    [SerializeField] uint ID;
    [SerializeField] new string name;
    [TextArea(3,5)]
    [SerializeField] string description;
    [TextArea(3,5)]
    [SerializeField] string actionMessage;
    [SerializeField] SkillType skillType;
    [SerializeField] int levelRequirement;
    [SerializeField] bool isUnlocked;
    [SerializeField] bool isUnlockable;
    [SerializeField] List<Skill> nextSkills = new List<Skill>();
    [SerializeField] bool hasStatusEffects;
    [SerializeField] List<StatusEffect> statusEffects = new List<StatusEffect>();

    public uint GetID() { return ID; }
    public SkillType GetSkillType() { return skillType; }
    public int GetLevelRequirement() { return levelRequirement; }
    public bool IsUnlocked() { return isUnlocked; }
    public bool IsUnlockable() { return isUnlockable; }
    public List<Skill> GetNextSkills() { return nextSkills; }
    public List<StatusEffect> GetStatusEffects() { return statusEffects; }
    public bool HasStatusEffects() { return hasStatusEffects; }

    public void SetUnlocked() { isUnlocked = true; }

    public void UnlockNextSkills()
    {
        foreach (Skill skill in nextSkills)
        {
            if(!Engine.SkillDictionary[skill.GetID()].IsUnlockable())
               Engine.SkillDictionary[skill.GetID()].isUnlockable = true;
        }
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public string GetActionMessage()
    {
        return actionMessage;
    }
}
