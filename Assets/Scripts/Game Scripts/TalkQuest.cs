using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Talk Quest", menuName = "Quests/Talk Quest", order = 1)]
public class TalkQuest : Quest
{

    [SerializeField] NPC targetNPC;
    [SerializeField] QuestDialogue targetNPCDialogue;

    public NPC GetTargetNPC() { return targetNPC; }
    public QuestDialogue GetTargetNPCDialogue() { return targetNPCDialogue; }

    public override bool CheckQuestCompletion(Player player)
    {       
        return false;
    }

    public bool CheckTalkCompletion(NPC npc)
    {
        return npc.GetID() == targetNPC.GetID();
    }

}
