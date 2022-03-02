using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("Basic Information")]
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

    [Header("Attributes")]    
    [SerializeField] int attributeCost;
    [SerializeField] Vector2Int damageRange;
    [SerializeField] List<StatusEffect> statusEffects = new List<StatusEffect>();

    bool isActive;

    public uint GetID() { return ID; }
    public SkillType GetSkillType() { return skillType; }
    public Sprite GetSprite() { return sprite; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public string GetCardDescription() { return cardDescription; }
    public string GetActionMessage() { return actionMessage; }
    public bool IsActive() { return isActive; }
    public int GetAttributeCost() { return attributeCost; }
    public int GetMinDamage() { return damageRange.x; }
    public int GetMaxDamage() { return damageRange.y; }

    /// <summary>
    /// Returns a random integer between damageRange.x [inclusive] and damageRange.y [inclusive]
    /// </summary>
    /// <returns> Return random integer between damageRange.x and damageRange.y [inclusive] </returns>
    public int GetDamageModifier() { return Random.Range(damageRange.x, damageRange.y + 1); }

    public List<StatusEffect> GetStatusEffects() { return statusEffects; }
    public bool HasStatusEffects() { return statusEffects != null && statusEffects.Count > 0 && !statusEffects.Contains(null); }
  
    public void SetActive(bool x) { isActive = x; }

}
