using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Location Quest", menuName = "Quests/Location Quest", order = 2)]
public class LocationQuest : Quest
{
    [SerializeField] Location location;
    
    public LocationQuest(Location location, Dialogue dialogue, QuestType questType, bool isCompleted, string name = "", string description = "", bool hasItemReward = false, uint id = 0, uint expReward = 0, uint goldReward = 0)
        : base(dialogue, questType, name, description, isCompleted, hasItemReward, id, expReward, goldReward)
    {
        this.location = location;
    }

    public Location GetLocation() { return location; }

    public void SetLocation(Location location) { this.location = location; }

    public override bool CheckQuestCompletion(Player player)
    {
        if (player.GetLocation() == location)
            return true;
        return false;
    }
}
