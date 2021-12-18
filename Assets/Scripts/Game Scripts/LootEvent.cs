using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Event", menuName = "Events/Loot Event", order = 4)]
public class LootEvent : Event
{

    [SerializeField] ItemLootTable lootTable;

    public ItemLootTable GetItemLootTable() { return lootTable; }
    public void SetItemLootTable(ItemLootTable lootTable) { this.lootTable = lootTable; }

}
