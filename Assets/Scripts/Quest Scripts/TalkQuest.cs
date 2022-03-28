using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Talk Quest", menuName = "Quests/Talk Quest", order = 1)]
public class TalkQuest : Quest
{

    [SerializeField] NPC _targetNPC;
    [SerializeField] NPC _sourceNPC;

    public NPC GetTargetNPC() { return _targetNPC; }
    public NPC GetSourceNPC() { return _sourceNPC; }


    public override bool CheckQuestCompletion()
    {       
        return false;
    }

    public bool CheckQuestCompletion(NPC npc)
    {
        return npc.GetID() == _targetNPC.GetID();
    }

}
