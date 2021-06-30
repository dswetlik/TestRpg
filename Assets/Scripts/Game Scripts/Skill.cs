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

    [SerializeField] new string name;
    [TextArea(3,5)]
    [SerializeField] string description;
    [TextArea(3,5)]
    [SerializeField] string actionMessage;
    [SerializeField] SkillType skillType;
    [SerializeField] bool isUnlocked;
    [SerializeField] bool isUnlockable;
    [SerializeField] List<Skill> nextSkills = new List<Skill>();

    public SkillType GetSkillType() { return skillType; }
    public bool IsUnlocked() { return isUnlocked; }
    public bool IsUnlockable() { return isUnlockable; }
    public List<Skill> GetNextSkills () { return nextSkills; }

    public void SetUnlocked() { isUnlocked = true; }

    public void UnlockNextSkills()
    {
        foreach (Skill skill in nextSkills)
            if (!skill.IsUnlockable())
                isUnlockable = true;
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
