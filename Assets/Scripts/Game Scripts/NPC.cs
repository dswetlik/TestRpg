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

    [SerializeField] new string name;
    [SerializeField] NPCType npcType;
    [SerializeField] Dialogue dialogue;
    [SerializeField] Store store;

    public string GetName() { return name; }
    public NPCType GetNPCType() { return npcType; }
    public Dialogue GetDialogue() { return dialogue; }
    public Store GetStore() { return store; }
}
