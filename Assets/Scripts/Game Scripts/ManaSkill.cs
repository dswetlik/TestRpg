using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mana Skill", menuName = "Skills/Mana Skill")]
public class ManaSkill : Skill
{

    public enum MagicType
    {
        none,
        damage,
        heal,
        effect,
        dispel
    }

    [SerializeField] MagicType magicType;

    public MagicType GetMagicType() { return magicType; }


}
