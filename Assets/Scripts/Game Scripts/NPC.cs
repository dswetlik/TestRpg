using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPC : ScriptableObject
{

    public enum NPCType
    {
        basic,
        merchant
    }

    [SerializeField] uint id;
    [SerializeField] new string name;
    [SerializeField] NPCType npcType;
    [SerializeField] bool hasQuests;
    [SerializeField] List<Quest> questList;
    [SerializeField] bool hasGivenQuest;
    [SerializeField] Quest givenQuest;
    [SerializeField] Dialogue dialogue;
    [SerializeField] Store store;

    public uint GetID() { return id; }
    public string GetName() { return name; }
    public NPCType GetNPCType() { return npcType; }

    public bool HasStore() { return store != null ? true : false; }
    public bool HasQuests() { return hasQuests; }
    public List<Quest> GetQuests() { return questList; }
    public bool HasGivenQuest() { return hasGivenQuest;}
    public Quest GetGivenQuest() { return givenQuest; }

    public Dialogue GetDialogue() { return dialogue; }
    public Store GetStore() { return store; }

    public void SetHasGivenQuest(bool x) { hasGivenQuest = x; }
    public void SetGivenQuest(Quest quest) { givenQuest = quest; }
}
