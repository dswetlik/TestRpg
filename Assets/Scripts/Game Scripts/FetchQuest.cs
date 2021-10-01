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

    [SerializeField] List<Item> fetchItems;
    [SerializeField] List<int> itemCounts;

    public List<Item> GetFetchItems() { return fetchItems; }
    public List<int> GetItemCounts() { return itemCounts; }

    public override bool CheckQuestCompletion(Player player)
    {
        for (int i = 0; i < fetchItems.Count; i++)
        {
            if (player.GetInventory().CheckForItem(fetchItems[i].GetID()) && !IsCompleted())
            {
                if (player.GetInventory().GetItemCount(fetchItems[i].GetID()) >= itemCounts[i])
                    continue;
                else
                    return false;
            }
            else
                return false;
        }
        return true;    
    }
}
