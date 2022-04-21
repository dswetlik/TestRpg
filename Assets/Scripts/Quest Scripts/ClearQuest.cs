using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Clear Quest", menuName = "Quests/Clear Quest")]
public class ClearQuest : Quest
{

    [SerializeField] Dungeon dungeon;

    public Dungeon GetDungeon() { return dungeon; }

    public override bool CheckQuestCompletion()
    {
        try
        {
            return (Engine.LocationDictionary[dungeon.GetID()] as Dungeon).IsCleared();
        }
        catch(KeyNotFoundException)
        {
            Debug.LogErrorFormat("Dungeon with ID {0} not found in dictionary. Returning false.");
            return false;
        }
    }

}
