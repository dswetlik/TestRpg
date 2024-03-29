﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Slay Quest", menuName = "Quests/Slay Quest", order = 2)]
public class SlayQuest : Quest
{

    [SerializeField] List<Enemy> slayEnemies;
    [SerializeField] List<int> numSlayEnemies;
    [SerializeField] List<int> countSlayEnemies;

    public List<Enemy> GetSlayEnemies() { return slayEnemies; }
    public List<int> GetNumSlayEnemies() { return numSlayEnemies; }
    public List<int> GetCountSlayEnemies() { return countSlayEnemies; }

    public void IncrementSlainEnemy(Enemy enemy)
    {
        for(int i = 0; i < slayEnemies.Count; i++)
        {           
            if (enemy.GetID() == slayEnemies[i].GetID())
                countSlayEnemies[i]++;
        }
    }

    public override bool CheckQuestCompletion()
    {
        for (int i = 0; i < slayEnemies.Count; i++)
        {
            if (numSlayEnemies[i] <= countSlayEnemies[i])
                continue;
            else
                return false;
        }
        return true;
    }
}
