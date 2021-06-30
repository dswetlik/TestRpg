using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContainer : MonoBehaviour
{
    [SerializeField] bool isEnemy;
    [SerializeField] ActiveSkill playerSkill;
    [SerializeField] EnemyAttackType enemySkill;

    public ActiveSkill GetPlayerSkill()
    {
        return playerSkill;
    }

    public EnemyAttackType GetEnemySkill()
    {
        return enemySkill;
    }

    public void SetPlayerSkill(ActiveSkill playerSkill)
    {
        this.playerSkill = playerSkill;
    }

    public void SetEnemySkill(EnemyAttackType enemySkill)
    {
        this.enemySkill = enemySkill;
    }
}
