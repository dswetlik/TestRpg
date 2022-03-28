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

    public override bool CheckQuestCompletion() { return Engine.DungeonDictionary[dungeon.GetID()].IsCleared(); }

}
