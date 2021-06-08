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
    [SerializeField] SkillType skillType;
    [SerializeField] bool isUnlocked;
    [SerializeField] List<Skill> prerequisteSkills = new List<Skill>();

    public SkillType GetSkillType() { return skillType; }
    public bool IsUnlocked() { return isUnlocked; }
    public List<Skill> GetPrerequisites() { return prerequisteSkills; }

    public bool CheckUnlockable()
    {
        foreach (Skill skill in prerequisteSkills)
            if (!skill.IsUnlocked())
                return false;
        return true;
    }


}
