using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{

    [SerializeField] Skill skill;

    public Skill GetSkill()
    {
        return skill;
    }

}
