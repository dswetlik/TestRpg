using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] Dialogue dialogue;

    public string GetName() { return name; }
    public Dialogue GetDialogue() { return dialogue; }
}
