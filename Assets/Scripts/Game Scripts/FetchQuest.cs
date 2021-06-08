using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Fetch Quest", menuName = "Quests/Fetch Quest", order = 1)]
public class FetchQuest : Quest
{

    [SerializeField] Item fetchItem;

    public FetchQuest(Item fetchItem, Dialogue dialogue, QuestType questType, string name = "", string description = "", bool isCompleted = false, bool hasItemReward = false, uint id = 0, uint expReward = 0, uint goldReward = 0)
        : base(dialogue, questType, name, description, isCompleted, hasItemReward, id, expReward, goldReward)
    {
        this.fetchItem = fetchItem;
    }

    public Item GetFetchItem() { return fetchItem; }

    public void SetItem(Item item) { fetchItem = item; }

    public override bool CheckQuestCompletion(Player player)
    {
        if (player.GetInventory().CheckForItem(fetchItem.GetID()))
            return true;
        return false;     
    }
}
