
using System.Collections.Generic;

interface ISkillable
{

    List<Skill> SkillList { get; set; }

    void AddSkill(Skill skill);

    List<StaminaSkill> GetStaminaSkills();

    List<ManaSkill> GetManaSkills();

}
