using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clear Quest", menuName = "Quests/Clear Quest")]
public class ClearQuest : Quest
{
    [SerializeField] Dungeon dungeon;

    public Dungeon GetDungeon() { return dungeon; }

    public override bool CheckQuestCompletion(Player player)
    {
        return Engine.DungeonDictionary[dungeon.GetID()].IsCleared();
    }
}
