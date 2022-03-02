using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContainer : MonoBehaviour
{
    [SerializeField] bool isEnemy;
    [SerializeField] Skill playerSkill;
    [SerializeField] EnemyAttackType enemySkill;

    public Skill GetPlayerSkill()
    {
        return playerSkill;
    }

    public EnemyAttackType GetEnemySkill()
    {
        return enemySkill;
    }

    public void SetPlayerSkill(Skill playerSkill)
    {
        this.playerSkill = playerSkill;
    }

    public void SetEnemySkill(EnemyAttackType enemySkill)
    {
        this.enemySkill = enemySkill;
    }
}
