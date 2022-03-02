using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Book", menuName = "Items/Consumables/Skill Book")]
public class SkillBook : Consumable
{

    public enum SkillBookType
    {
        skill,
        magic
    }

    [SerializeField] SkillBookType skillBookType;
    [SerializeField] uint skillID;

    public override void UseItem(Player player)
    {
        if (!player.GetSkills().Contains(Engine.SkillDictionary[skillID]))
        {
            player.AddSkill(Engine.SkillDictionary[skillID]);
            GameObject.Find("GameManager").GetComponent<Engine>().AddSkill(Engine.SkillDictionary[skillID]);
        }
    }
}
