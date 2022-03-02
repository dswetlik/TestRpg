using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stamina Skill", menuName = "Skills/Stamina Skill")]
public class StaminaSkill : Skill
{

    [SerializeField] bool isWeaponModifier;

    public bool IsWeaponModifier() { return isWeaponModifier; }

}
