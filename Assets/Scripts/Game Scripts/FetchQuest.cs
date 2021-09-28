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
    [SerializeField] int itemCount;

    public Item GetFetchItem() { return fetchItem; }
    public int GetItemCount() { return itemCount; }

    public void SetItem(Item item) { fetchItem = item; }

    public override bool CheckQuestCompletion(Player player)
    {
        if (player.GetInventory().CheckForItem(fetchItem.GetID()) && !IsCompleted())
            if (player.GetInventory().GetItemCount(fetchItem.GetID()) >= itemCount)
                return true;
        return false;     
    }
}
