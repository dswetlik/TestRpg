using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
[CreateAssetMenu(fileName = "New Location Quest", menuName = "Quests/Location Quest", order = 2)]
public class LocationQuest : Quest
{
    [SerializeField] Location location;

    public Location GetLocation() { return location; }

    public void SetLocation(Location location) { this.location = location; }

    public override bool CheckQuestCompletion() { return false; }

    public bool CheckQuestCompletion(Player player)
    {
        if (player.GetLocation() == location)
            return true;
        return false;
    }
}
