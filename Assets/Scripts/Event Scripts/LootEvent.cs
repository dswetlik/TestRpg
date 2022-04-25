using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Event", menuName = "Events/Loot Event", order = 4)]
public class LootEvent : Event, IEventLinkable, IEventDescription, IEventPlayer
{

    protected LootEvent() : base(EventType.loot) { }

    [SerializeField] [TextArea(3, 5)] string playerDescription;
    [SerializeField] string eventLinkButtonText;
    [SerializeField] [TextArea(3, 5)] string eventDescription;

    [SerializeField] List<Event> eventLinks;

    [SerializeField] ItemLootTable lootTable;

    public string GetPlayerDescription() { return playerDescription; }
    public string GetEventLinkButtonText() { return eventLinkButtonText; }

    public string GetEventDescription() { return eventDescription; }

    public List<Event> GetEventLinks() { return eventLinks; }

    public ItemLootTable GetItemLootTable() { return lootTable; }
    public void SetItemLootTable(ItemLootTable lootTable) { this.lootTable = lootTable; }

}
